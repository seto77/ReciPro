using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Crystallography.Controls;

[Serializable()]
public class MacroTriger(string target, bool debug, object[] obj, string macroName = "")
{
    public bool Debug { set; get; } = debug;
    public string Target { set; get; } = target;
    public string MacroName = macroName;
    public object[] Obj = obj;
}

[Serializable()]
public class MacroBase
{
    public dynamic mainObject;
    public string[] Help => [.. help];
    public string ScopeName = "";
    public List<string> help = [];

    public MacroBase(dynamic _main, string scopeName)
    {
        mainObject = _main;
        ScopeName = scopeName;
    }

    public void SetMacroToMenu(string[] name)
    {
        mainObject.SetMacroToMenu(name);
    }
}

[Serializable()]
public class MacroSub
{
    private Control context;

    public MacroSub(Control _context) => context = _context;

    //スレッド間で安全にコントロールを操作する、関数群
    public Type Execute<Type>(Expression<Func<Type>> expression) => Execute<Type>(context, expression.Compile(), null);

    public void Execute(Expression<Action> expression) => Execute(context, expression.Compile(), null);

    //public bool Execute(Func<bool> func) => Execute<bool>(func);

    //public string Execute(Func<string> func) => Execute<string>(func);

    //public string[] Execute(Func<string[]> func) => Execute<string[]>(func);

    // public double[] Execute(Func<double[]> func) => Execute<double[]>(func);

    //public int[] Execute(Func<int[]> func) => Execute<int[]>(func);

    //public int Execute(Func<int> func) => Execute<int>(func);

    //public double Execute(Func<double> func) => Execute<double>(func);

    public static Type Execute<Type>(Control _context, Delegate process) => Execute<Type>(_context, process, null);

    public Type Execute<Type>(Delegate process) => Execute<Type>(context, process, null);

    public static void Execute(Control _context, Delegate process) => Execute(_context, process, null);

    public void Execute(Delegate process) => Execute(context, process, null);

    public static Type Execute<Type>(Control _context, Delegate process, params object[] args)
    {
        #region
        /*if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            if (!(context.IsHandleCreated))
            {
                return null;
            }
            */
        #endregion
        return _context.InvokeRequired ? (Type)_context.Invoke(process, args) : (Type)process.DynamicInvoke(args);
    }

    public static void Execute(Control _context, Delegate process, params object[] args)
    {
        #region
        /*if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            if (!(context.IsHandleCreated))
            {
                return null;
            }
            */
        #endregion
        if (_context.InvokeRequired)
            _context.Invoke(process, args);
        else
            process.DynamicInvoke(args);
    }

    public Type Execute<Type>(Delegate process, params object[] args)
    {
        #region
        /*if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            if (!(context.IsHandleCreated))
            {
                return null;
            }
            */
        #endregion
        return context.InvokeRequired ? (Type)context.Invoke(process, args) : (Type)process.DynamicInvoke(args);
    }

    public void Execute(Delegate process, params object[] args)
    {
        #region
        /*if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            if (!(context.IsHandleCreated))
            {
                return null;
            }
            */
        #endregion
        if (context.InvokeRequired)
            context.Invoke(process, args);
        else
            process.DynamicInvoke(args);
    }
}