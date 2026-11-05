"""Extract public macro API from ReciPro/Macro/Macro.cs and compare with docs built-in-functions page."""
import re, sys
src = open(sys.argv[1], encoding='utf-8-sig').read().split('\n')
doc = open(sys.argv[2], encoding='utf-8').read()
cls = None; bases = {}; members = {}  # cls -> list
pending_help = None
fieldmap = {}  # Macro field name -> class
for i, line in enumerate(src):
    m = re.match(r'    public (?:abstract )?class (\w+)(?:\([^)]*\))?\s*:\s*(\w+)', line)
    if m:
        cls = m.group(1); bases[cls] = m.group(2); members.setdefault(cls, []); continue
    m = re.match(r'    public (\w+) (\w+);', line)
    if m: fieldmap[m.group(2)] = m.group(1)
    h = re.search(r'\[Help\(\$?"((?:[^"\\]|\\.)*)"(?:\s*,\s*"([^"]*)")?', line)
    if h: pending_help = (h.group(1), h.group(2) or '', i+1)
    elif re.search(r'\[Help\(', line): pending_help = ('(multi-line)', '?', i+1)
    m = re.match(r'(\s+)public (?:static |override |virtual )?([\w\[\]<>,\. ]+?) (\w+)\s*(\(([^)]*)\)?|\{|=>)', line)
    if m and cls and m.group(3) not in ('class',) and not line.strip().startswith('public class') and len(m.group(1)) in (4,8):
        indent=len(m.group(1))
        name = m.group(3); rtype = m.group(2)
        if name == cls: pending_help=None; continue
        kind = 'method' if m.group(4).startswith('(') else 'property'
        params = m.group(5) if kind=='method' else ''
        if kind=='method' and not line.rstrip().endswith((')', '{')) and ')' not in line[line.index(name):]:
            # multi-line params
            j=i; buf=line[line.index('(')+1:]
            while ')' not in buf: j+=1; buf+=' '+src[j].strip()
            params = buf[:buf.index(')')]
        acc=''
        if kind=='property':
            rest=line[line.index(name):]
            if '=>' in rest.split('{')[0]: acc='get'
            else:
                blk=' '.join(src[i:i+12]); acc=('get' if 'get' in blk.split('}')[0] or 'get' in blk[:300] else '')+('/set' if re.search(r'\bset\b', blk[:400]) else '')
        owner = 'ROOT' if indent==4 else cls
        members.setdefault(owner, []).append(dict(name=name, kind=kind, rtype=rtype, params=' '.join(params.split()), acc=acc, help=pending_help, line=i+1))
        pending_help=None
def allmembers(c):
    out=[]; 
    while c in members:
        out = members[c] + out; c = bases.get(c)
    return out
print('# fields:', fieldmap)
rows=[]
for field, c in list(fieldmap.items()):
    for mm in allmembers(c):
        rows.append((field, mm))
for mm in members.get('ROOT', []): rows.append(('', mm))
for field, mm in rows:
    full = (field+'.' if field else '') + mm['name']
    h = mm['help']
    inDoc = bool(re.search(r'(?<![\w])'+re.escape(mm['name'])+r'(?![\w])', doc))
    hs = ('HELP' if h else 'NOHELP')
    print(f"{full:45s} {mm['kind']:8s} {mm['rtype']:10s} ({mm['params']}) {mm['acc']:7s} {hs:6s} helpArg=({h[1] if h else ''}) doc={'Y' if inDoc else 'N'} L{mm['line']}")
