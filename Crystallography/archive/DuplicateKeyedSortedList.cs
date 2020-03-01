using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace Crystallography
{
    /// <summary>
    /// キーによってソートされているリストです. キーはリストの各項目から導出されます. キーは重複させることができます.
    /// </summary>
    /// <typeparam name="TKey">リストのキーの型です.</typeparam>
    /// <typeparam name="TItem">リストの項目の型です.</typeparam>
    /// <remarks>このクラスは, 継承したクラスを作成して使用します.</remarks>
    [Serializable, DebuggerDisplay("Count = {Count}")]
    public abstract class DuplicateKeyedSortedList<TKey, TItem>
        : ICollection<TItem>, IDictionary<TKey, TItem>, IDictionary, ISerializable, IDeserializationCallback
    {
        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0CannotCast1 = "{0} を {1} にキャストできません.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_Key0NotFoundIn1 = "キー {0} は {1} 内に見つかりません.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0LessThan1 = "{0} が {1} 未満です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0EqualsGreaterThan1 = "{0} が {1} 以上です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0Is1 = "{0} が {1} です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0GreaterThan1 = "{0} が {1} を超過しています.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_2From0And1LessThan3 = "{0} と {1} から取得される {2} が {3} 未満です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0HasTypeInvalid = "{0} の要素の型が不正です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_2From0And1EqualsMoreThan3 = "{0} と {1} から取得される {2} が {3} 以上です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0Has1FromIs2 = "{0} の要素に, 取得される {1} が {2} のものが存在します.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_NotSupportedBecauseOf0 = "{0} のためサポートされません.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_SerializationInfoNull = "逆シリアル化完了イベントが呼び出されましたが, 逆シリアル化情報が null です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_SerializationMissingData = "逆シリアル化に必要なデータを正しく取得できません.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_SerializationKeyNull = "シリアル化データにキーが null の項目が含まれています.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_SerializationKeyMissSort = "シリアル化データ内でキーが正しくソートされていません.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0True = "{0} が true です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_IEnumerator_CurrentOutOfRange = "現在位置が最初の要素の前, または最後の要素の後です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0And1NotConsistent = "{0} と {1} の間に整合性がありません.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_1From0Is2 = "{0} から取得される {1} が {2} です.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_0HasInvalid = "{0} の要素に不正なものが存在します.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string ExceptionMessage_1From0And2NotConsistent = "{0} から取得される {1} と {2} の間に整合性がありません.";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string String_ReadOnly = "読み取り専用";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string String_Key = "キー";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string String_MultiDimension = "多次元";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string String_IndexNumber = "インデックス番号";

        /// <summary>
        /// 例外スロー時のメッセージ文字列です.
        /// </summary>
        private const string String_ItemCount = "項目数";

        /// <summary>
        /// 容量の初期値です.
        /// </summary>
        private const int CapacityDefault = 10;

        /// <summary>
        /// 空のキー配列です.
        /// </summary>
        private static readonly TKey[] EmptyKeys = new TKey[0];

        /// <summary>
        /// 空の値配列です.
        /// </summary>
        private static readonly TItem[] EmptyValues = new TItem[0];

        /// <summary>
        /// キー配列です.
        /// </summary>
        /// <remarks>配列が null の要素を含まないようにしてください.</remarks>
        private TKey[] keys;

        /// <summary>
        /// 項目の配列です.
        /// </summary>
        private TItem[] items;

        /// <summary>
        /// 外部に公開する, DuplicateKeyedSortedList のキーのコレクションです.
        /// </summary>
        private DuplicateKeyedSortedList<TKey, TItem>.KeyCollection keyCollection;

        /// <summary>
        /// 外部に公開する, DuplicateKeyedSortedList の値のコレクションです.
        /// </summary>
        private DuplicateKeyedSortedList<TKey, TItem>.ValueCollection valueCollection;

        /// <summary>
        /// キー配列, 値配列の要素のうち, 外部から見て有効な要素の数です.
        /// </summary>
        private int size;

        /// <summary>
        /// キーの比較演算子です.
        /// </summary>
        /// <remarks>このメンバを使用する際は, null チェックは不要です. このメンバは null にしないでください.</remarks>
        private IComparer<TKey> comparer;

        /// <summary>
        /// DuplicateKeyedSortedList へのアクセスを同期するために使用するオブジェクトです.
        /// </summary>
        private object syncRoot;

        /// <summary>
        /// 逆シリアル化に必要な情報です.
        /// </summary>
        private SerializationInfo serializationInfo;

        /// <summary>
        /// 空で, 既定の容量を備え, 既定のキー比較演算子を使用する, DuplicateKeyedSortedList の新しいインスタンスを初期化します.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">dictionary が null です.</exception>
        protected DuplicateKeyedSortedList()
            : this((IComparer<TKey>)null)
        {
        }

        /// <summary>
        /// 空で, 既定の容量を備え, 指定されたキー比較演算子を使用する, DuplicateKeyedSortedList の新しいインスタンスを初期化します.
        /// </summary>
        /// <param name="comparer">キーをソートする際の比較に使用する比較演算子です. 既定の比較演算子を使用する場合は, null です.</param>
        /// <exception cref="System.ArgumentNullException">dictionary が null です.</exception>
        protected DuplicateKeyedSortedList(IComparer<TKey> comparer)
            : this(DuplicateKeyedSortedList<TKey, TItem>.CapacityDefault, comparer)
        {
        }

        /// <summary>
        /// 空で, 指定された容量を備え, 既定のキー比較演算子を使用する, DuplicateKeyedSortedList の新しいインスタンスを初期化します.
        /// </summary>
        /// <param name="capacity">容量です. 容量は, DuplicateKeyedSortedList が内部で管理するキー配列, 値配列の要素数です.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity が 0 未満です.</exception>
        protected DuplicateKeyedSortedList(int capacity)
            : this(capacity, null)
        {
        }

        /// <summary>
        /// 空で, 指定された容量を備え, 指定されたキー比較演算子を使用する, DuplicateKeyedSortedList の新しいインスタンスを初期化します.
        /// </summary>
        /// <param name="capacity">容量です. 容量は, DuplicateKeyedSortedList が内部で管理するキー配列, 値配列の要素数です.</param>
        /// <param name="comparer">キーをソートする際の比較に使用する比較演算子です. 既定の比較演算子を使用する場合は, null です.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity が 0 未満です.</exception>
        protected DuplicateKeyedSortedList(int capacity, IComparer<TKey> comparer)
        {
            // 引数チェック
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "capacity", "0"));
            }

            // キー配列, 値配列を作成し, 有効サイズを初期化します.
            this.keys = new TKey[capacity];
            this.items = new TItem[capacity];
            this.size = 0;
            this.comparer = comparer ?? Comparer<TKey>.Default;
        }

        /// <summary>
        /// 指定された SerializationInfo と StreamContext を使用して逆シリアル化できる, DuplicateKeyedSortedList の新しいインスタンスを初期化します.
        /// </summary>
        /// <param name="info">シリアル化に必要な情報です.</param>
        /// <param name="context">関連付けられているシリアル化ストリームのソースおよびデスティネーションを格納している StreamingContext です.</param>
        /// <remarks>逆シリアル化を行う際以外では使用しないで下さい.</remarks>
        protected DuplicateKeyedSortedList(SerializationInfo info, StreamingContext context)
        {
            this.serializationInfo = info;
        }

        /// <summary>
        /// リストが変更されたときに発生するイベントです.
        /// </summary>
        private event EventHandler ListModified;

        /// <summary>
        /// DuplicateKeyedSortedList のキーのコレクションを取得します.
        /// </summary>
        /// <value>DuplicateKeyedSortedList のキーを格納しているコレクションです.</value>
        public ICollection<TKey> Keys
        {
            get
            {
                if (this.keyCollection == null)
                {
                    this.keyCollection = new DuplicateKeyedSortedList<TKey, TItem>.KeyCollection(this);
                }

                return this.keyCollection;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の値のコレクションを取得します.
        /// </summary>
        /// <value>DuplicateKeyedSortedList の値を格納しているコレクションです.</value>
        public ICollection<TItem> Values
        {
            get
            {
                if (this.valueCollection == null)
                {
                    this.valueCollection = new DuplicateKeyedSortedList<TKey, TItem>.ValueCollection(this);
                }

                return this.valueCollection;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList のキーのコレクションを取得します.
        /// </summary>
        /// <value>DuplicateKeyedSortedList のキーを格納しているコレクションです.</value>
        ICollection IDictionary.Keys
        {
            get
            {
                if (this.keyCollection == null)
                {
                    this.keyCollection = new DuplicateKeyedSortedList<TKey, TItem>.KeyCollection(this);
                }

                return this.keyCollection;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の値のコレクションを取得します.
        /// </summary>
        /// <value>DuplicateKeyedSortedList の値を格納しているコレクションです.</value>
        ICollection IDictionary.Values
        {
            get
            {
                if (this.valueCollection == null)
                {
                    this.valueCollection = new DuplicateKeyedSortedList<TKey, TItem>.ValueCollection(this);
                }

                return this.valueCollection;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList に格納されている要素の数を取得します.
        /// </summary>
        /// <value>DuplicateKeyedSortedList に格納されている要素の数です.</value>
        public int Count
        {
            get
            {
                // 要素数を取得して返す.
                return this.size;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList のソートの際に使用される比較演算子を取得します.
        /// </summary>
        /// <value>DuplicateKeyedSortedList のソートの際に使用される比較演算子です.</value>
        public IComparer<TKey> Comparer
        {
            get
            {
                // 比較演算子を取得して返す.
                return this.comparer;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の容量を取得, 設定します. 容量は, DuplicateKeyedSortedList が内部で管理するキー配列, 値配列の要素数です.
        /// </summary>
        /// <value>DuplicateKeyedSortedList の容量です</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Capacity を現在の DuplicateKeyedSortedList の要素数より小さい値に設定しようとしました.</exception>
        /// <remarks>容量は, DuplicateKeyedSortedList が内部で管理するキー配列, 値配列の要素数です. つまり, これらの配列を変更せずに格納できる要素の数を表します.</remarks>
        public int Capacity
        {
            get
            {
                // キー配列の現在の容量を取得して返す.
                return this.keys.Length;
            }

            set
            {
                // 値を変更していない場合は終了
                if (value == this.keys.Length)
                {
                    return;
                }

                // 容量を有効サイズより小さい値に設定しようとした場合, 例外スロー.
                if (value < this.size)
                {
                    throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "value", "this._size"));
                }

                // 容量を0に設定した場合は, 内部配列を空配列にする.
                // そうでない場合は, 指定された容量を持った配列を新たに作成し, そこに内容をコピーする
                if (value == 0)
                {
                    this.keys = DuplicateKeyedSortedList<TKey, TItem>.EmptyKeys;
                    this.items = DuplicateKeyedSortedList<TKey, TItem>.EmptyValues;
                }
                else
                {
                    //// 容量を変更した新たな配列を作成し, そこに内容をコピー.
                    TKey[] newKeys = new TKey[value];
                    TItem[] newValues = new TItem[value];
                    if (this.size > 0)
                    {
                        Array.Copy(this.keys, newKeys, this.size);
                        Array.Copy(this.items, newValues, this.size);
                    }

                    //// キー配列, 値配列として, 新たに作成した配列を設定.
                    this.keys = newKeys;
                    this.items = newValues;
                }
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList が読み取り専用であるかを取得します.
        /// </summary>
        /// <value>
        /// <para>DuplicateKeyedSortedList が読み取り専用である場合, true. そうでない場合, false です.</para>
        /// </value>
        /// <remarks>DuplicateKeyedSortedList では, 読み取り専用とする機能はサポートされていないため, この値は常に false です.</remarks>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// IDictionary が固定サイズであるかを取得します.
        /// </summary>
        /// <value>
        /// <para>IDictionary が固定サイズである場合, true. そうでない場合, false です.</para>
        /// </value>
        /// <remarks>DuplicateKeyedSortedList は, 固定サイズではないため, この値は常に false です.</remarks>
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ICollection に格納されている要素の数を返します.
        /// </summary>
        /// <value>ICollection に格納されている要素の数です.</value>
        int ICollection.Count
        {
            get
            {
                return this.Count;
            }
        }

        /// <summary>
        /// ICollection へのアクセスが複数スレッド間で同期されている（スレッドセーフである）かを取得します.
        /// </summary>
        /// <value>
        /// <para>ICollection へのアクセスが複数スレッド間で同期されている（スレッドセーフである）場合は true. そうでない場合は false です.</para>
        /// </value>
        /// <remarks>DuplicateKeyedSortedList では, アクセス同期機能はサポートされないため, この値は常に false です.</remarks>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// <para>DuplicateKeyedSortedList へのアクセスをスレッド間で同期するために使用するオブジェクトを取得します.</para>
        /// </summary>
        /// <value>DuplicateKeyedSortedList へのアクセスを複数スレッド間で同期するために使用するオブジェクトです.</value>
        public object SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref this.syncRoot, new object(), null);
                }

                return this.syncRoot;
            }
        }

        // TODO MidiEditor 開発のために使用不可にしている。あとで戻す
#if false
		/// <summary>
		/// 指定されたキーを持つ要素のうちインデックスが最も小さいものの値を取得, 設定します.
		/// </summary>
		/// <param name="key">キーです.</param>
		/// <returns>指定されたキーに対応する値です.</returns>
		/// <exception cref="System.ArgumentNullException">key が null です.</exception>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException">key が DuplicateKeyedSortedList 内に見つかりません.</exception>
		/// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
		public TItem this[TKey key]
		{
			get
			{
				// 引数チェック
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}

				// 指定されたキーを持つ要素を持つ最初のインデックスを取得.
				int index = this.IndexOfKey(key);

				// 指定されたキーを持つ要素が見つからない場合, 例外スロー.
				if (index < 0)
				{
					throw new KeyNotFoundException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_KeyNotFoundInCollection, "key"));
				}

				// そのインデックスの値を返します.
				return this._items[index];
			}
			set
			{
				// 入力チェック
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!this.ValidateKeyItemPair(key, value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_KeyNotForItem, "key", "value"), "value");
				}

				// 指定されたキーを持つ要素を持つ最初のインデックスを返します.
				int index = this.IndexOfKey(key);

				// 指定されたキーを持つ要素が見つからない場合, keyより大きい最初の要素のインデックス位置に新たに要素を追加.
				// 見つかった場合はそのインデックスに値を設定.
				if (index < 0)
				{
					this.Insert(~index, key, value, true);
				}
				else
				{
					this._items[index] = value;
				}
			}
		}
#endif

        // TODO MidiEditor 開発のために追加しているメソッド。あとで削除する

        /// <summary>
        /// ※MidiEditor 開発のために追加しているインデクサ. あとで削除する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>項目</returns>
        TItem IDictionary<TKey, TItem>.this[TKey key]
        {
            get
            {
                // 引数チェック
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }

                // 指定されたキーを持つ要素を持つ最初のインデックスを取得.
                int index = this.IndexOfKey(key);

                // 指定されたキーを持つ要素が見つからない場合, 例外スロー.
                if (index < 0)
                {
                    throw new KeyNotFoundException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_Key0NotFoundIn1, "key", "this"));
                }

                // そのインデックスの値を返します.
                return this.items[index];
            }

            set
            {
                // 入力チェック
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }

                if (!this.ValidateKeyItemPair(key, value))
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "key", "value"), "value");
                }

                // 指定されたキーを持つ要素を持つ最初のインデックスを返します.
                int index = this.IndexOfKey(key);

                // 指定されたキーを持つ要素が見つからない場合, keyより大きい最初の要素のインデックス位置に新たに要素を追加.
                // 見つかった場合はそのインデックスに値を設定.
                if (index < 0)
                {
                    this.Insert(~index, key, value, true);
                }
                else
                {
                    this.items[index] = value;
                }
            }
        }

        /// <summary>
        /// 指定されたキーに対応する項目を取得, 設定します.
        /// </summary>
        /// <param name="key">キーです.</param>
        /// <returns>指定されたキーに対応する値です.</returns>
        /// <exception cref="System.ArgumentException">key または value の型が, このコレクションのキー, または値の型にキャストできない型です.</exception>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">key が DuplicateKeyedSortedList 内に存在しません.</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        object IDictionary.this[object key]
        {
            get
            {
                // 引数チェック
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }

                // キーのTKeyへのキャストを試みる. できない場合は例外スロー.
                TKey concreteKey;
                try
                {
                    concreteKey = (TKey)key;
                }
                catch (InvalidCastException e)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0CannotCast1, "key", "TKey"), "key", e);
                }

                // 指定されたキーを持つ要素を持つ最初のインデックスを取得.
                int index = this.IndexOfKey(concreteKey);

                // 指定されたキーを持つ要素が見つからない場合は例外スロー.
                if (index < 0)
                {
                    throw new KeyNotFoundException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_Key0NotFoundIn1, "key", "this"));
                }

                // そのインデックスの値を返します.
                return this.items[index];
            }

            set
            {
                // 引数チェック.
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }

                // TKeyへのキャストを試みる. できない場合は例外スロー.
                TKey concreteKey;
                try
                {
                    concreteKey = (TKey)key;
                }
                catch (InvalidCastException e)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0CannotCast1, "key", "TKey"), "key", e);
                }

                // TItem へのキャストを試みる. できない場合は例外スロー.
                TItem concreteItem;
                try
                {
                    concreteItem = (TItem)value;
                }
                catch (InvalidCastException e)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0CannotCast1, "value", "TItem"), "value", e);
                }

                // value のキーが key になっているかをチェック
                if (!this.ValidateKeyItemPair(concreteKey, concreteItem))
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "key", "value"), "value");
                }

                // 指定されたキーを持つ要素を持つ最初のインデックスを取得.
                int index = this.IndexOfKey(concreteKey);

                // 指定されたキーを持つ要素が見つからない場合, key より大きい最初の要素のインデックス位置に追加
                // 見つかった場合は, そのインデックスに値を設定.
                if (index < 0)
                {
                    this.Insert(~index, concreteKey, concreteItem, true);
                }
                else
                {
                    this.items[index] = concreteItem;
                }
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList に項目を追加します. 追加先インデックス位置は, 自動的に計算されます.
        /// </summary>
        /// <param name="item">追加する項目です.</param>
        /// <returns>新しい要素が挿入されたインデックス位置です.</returns>
        /// <exception cref="System.ArgumentException">項目 item のキーが null です.</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        public int Add(TItem item)
        {
            // 引数チェック
            TKey key = this.GetKeyForItem(item);
            if (key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            // 挿入先インデックスを取得.
            int insertIndex = this.GetInsertIndex(key);

            // 挿入処理
            this.Insert(insertIndex, key, item, true);

            // 新しい要素が挿入された位置を返します.
            return insertIndex;
        }

        /// <summary>
        /// <para>DuplicateKeyedSortedList に項目を追加します. 追加先インデックス位置は, 自動的に計算されます.</para>
        /// <para>キーが等価の項目がある場合, その項目の中の指定された位置に追加します.</para>
        /// </summary>
        /// <param name="item">追加する項目です.</param>
        /// <param name="indexIn">
        /// <para>キーが等価の項目がある場合の, その項目の中での追加先インデックスです.</para>
        /// <para>キーが等価の項目のうち最初のもののインデックス位置を 0 とした相対インデックスです.</para>
        /// <para>キーが等価の項目の数より大きい値を指定した場合は, それらの項目の末尾に挿入されます. 例外はスローされません.</para>
        /// </param>
        /// <returns>新しい要素が挿入された位置です.</returns>
        /// <exception cref="System.ArgumentException">項目 item のキーが null です.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">indexIn が 0 未満です.</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        public int AddByIndexIn(TItem item, int indexIn)
        {
            // 引数チェック
            if (indexIn < 0)
            {
                throw new ArgumentOutOfRangeException("indexIn", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "indexIn", "0"));
            }

            TKey key = this.GetKeyForItem(item);
            if (key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            // 挿入先インデックス取得
            int insertIndex = this.GetInsertIndex(key, indexIn);

            // 挿入処理
            this.Insert(insertIndex, key, item, true);

            // 新しい要素が挿入された位置を返します.
            return insertIndex;
        }

        /// <summary>
        /// DuplicateKeyedSortedList に項目を追加します. 追加先インデックス位置は, 自動的に計算されます.
        /// </summary>
        /// <param name="key">追加先のキーです.</param>
        /// <param name="item">追加する値です.</param>
        /// <exception cref="System.ArgumentException">キー key が項目 item のキーと一致しません.</exception>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        void IDictionary<TKey, TItem>.Add(TKey key, TItem item)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (!this.ValidateKeyItemPair(key, item))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "key", "item"), "key");
            }

            // 挿入先インデックスを取得.
            int insertIndex = this.GetInsertIndex(key);

            // 挿入処理
            this.Insert(insertIndex, key, item, true);
        }

        /// <summary>
        /// IDictionary に項目を追加します. 追加先インデックスは, 自動的に計算します.
        /// </summary>
        /// <param name="key">追加先のキーです.</param>
        /// <param name="item">追加する項目です.</param>
        /// <exception cref="System.ArgumentException">キーまたは値の型が, この DuplicateKeyedSortedList のキーまたは値の型にキャストできない型です. または, キー key が項目 item のキーと一致しません.</exception>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        void IDictionary.Add(object key, object item)
        {
            // null チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // キーのキャストを試みる.
            TKey concreteKey;
            try
            {
                concreteKey = (TKey)key;
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0CannotCast1, "key", "TKey"), "key", e);
            }

            // 値のキャストを試みる.
            TItem concreteItem;
            try
            {
                concreteItem = (TItem)item;
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0CannotCast1, "item", "TItem"), "item", e);
            }

            // キーが項目のキーであるかチェック
            if (!this.ValidateKeyItemPair(concreteKey, concreteItem))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "key", "item"), "key");
            }

            // 追加処理
            ((IDictionary<TKey, TItem>)this).Add(concreteKey, concreteItem);
        }

        /// <summary>
        /// DuplicateKeyedSortedList に項目を追加します. 追加先インデックス位置は, 自動的に計算されます.
        /// </summary>
        /// <param name="item">追加する項目です.</param>
        /// <exception cref="System.ArgumentException">項目 item のキーが null です.</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        void ICollection<TItem>.Add(TItem item)
        {
            // 引数チェック
            TKey key = this.GetKeyForItem(item);
            if (key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            this.Add(item);
        }

        /// <summary>
        /// ICollection に項目を追加します. 追加先インデックス位置は, 自動的に計算します.
        /// </summary>
        /// <param name="item">追加する項目です.</param>
        /// <exception cref="System.ArgumentException">item.Key が null です. または, item.Key が, item.Value のキーと等価でありません.</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        void ICollection<KeyValuePair<TKey, TItem>>.Add(KeyValuePair<TKey, TItem> item)
        {
            // 引数チェック
            if (item.Key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0Is1, "item.Key", "null"), "item");
            }

            if (!this.ValidateKeyItemPair(item.Key, item.Value))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "item"), "item");
            }

            // 追加処理
            this.Add(item.Value);
        }

        /// <summary>
        /// <para>この DuplicateKeyedSortedList に, キー, 値を追加します.</para>
        /// <para>等価のキーが存在する場合, 指定された値比較演算子により, インデックス位置を決定します.</para>
        /// </summary>
        /// <param name="item">追加する値です.</param>
        /// <param name="itemComparer">重複キー存在時のインデックス位置決定に用いる値比較演算子です. 既定の演算子を用いる場合, null です.</param>
        /// <returns>要素が追加されたインデックス位置です.</returns>
        /// <exception cref="System.ArgumentException">項目 item のキーが null です.</exception>
        /// <remarks>
        /// <para>等価のキーの中で昇順にソートされていない場合, 挿入位置は, 等価のキーの要素の中の予期せぬ位置となります.</para>
        /// <para>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</para>
        /// </remarks>
        public int AddByItemCompare(TItem item, IComparer<TItem> itemComparer)
        {
            // 引数チェック
            TKey key = this.GetKeyForItem(item);
            if (key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            if (itemComparer == null)
            {
                itemComparer = Comparer<TItem>.Default;
            }

            // 指定されたキーを持つインデックス範囲を取得します.
            ListIndexRange indexRange = this.GetIndexRangeOfKey(key);
            int index = indexRange.Index;
            int count = indexRange.Count;

            // インデックス範囲の中で, 値比較演算子の比較結果に従い, 挿入すべき位置を取得します.
            //// インデックス範囲の要素を抽出してインデックス位置を取得
            TItem[] itemArray = this.ToArray(index, count);
            int insertIndex = Array.BinarySearch<TItem>(itemArray, item, itemComparer);
            if (insertIndex < 0)
            {
                insertIndex = ~insertIndex;
            }
            //// インデックス位置をリスト全体からみたインデックス位置に補正
            insertIndex += index;

            // 要素を追加
            this.Insert(insertIndex, key, item, true);

            // 新しい要素が挿入された位置を返します.
            return insertIndex;
        }

        /// <summary>
        /// 指定された KeyValuePair 配列の要素すべてを, DuplicateKeyedSortedList に追加します.
        /// </summary>
        /// <param name="keyValuePairs">DuplicateKeyedSortedList に追加する要素を含む KeyValuePair 配列です..</param>
        /// <exception cref="System.ArgumentException">keyValuePairs の要素にキーが null のものが存在します.</exception>
        /// <exception cref="System.ArgumentNullException">keyValuePairs が null です.</exception>
        /// <remarks>
        /// <para>各要素追加完了後に, ListModified, ItemInserted イベントが発生します.</para>
        /// </remarks>
        public void AddAll(KeyValuePair<TKey, TItem>[] keyValuePairs)
        {
            // 引数チェック
            if (keyValuePairs == null)
            {
                throw new ArgumentNullException("keyValuePairs");
            }

            // dictionary からリスト項目を取得
            // 各 item の key の入れるべき位置インデックスを取得し, そのインデックスに挿入. -1 の場合は末尾
            foreach (KeyValuePair<TKey, TItem> keyValuePair in keyValuePairs)
            {
                if (keyValuePair.Key == null)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0Has1FromIs2, "keyValuePairs", String_Key, "null"), "keyValuePairs");
                }

                if (!this.ValidateKeyItemPair(keyValuePair.Key, keyValuePair.Value))
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0HasInvalid, "keyValuePairs"), "keyValuePairs");
                }

                int insertIndex = this.GetHigherIndex(keyValuePair.Key);
                if (insertIndex == -1)
                {
                    this.Insert(this.size, keyValuePair.Key, keyValuePair.Value, false);
                }
                else
                {
                    this.Insert(insertIndex, keyValuePair.Key, keyValuePair.Value, false);
                }
            }

            // 挿入した項目のリストを取得する.
            List<TItem> insertedItemList = new List<TItem>();
            foreach (KeyValuePair<TKey, TItem> keyValuePair in keyValuePairs)
            {
                insertedItemList.Add(keyValuePair.Value);
            }

            // イベント発生
            this.OnListModified(new EventArgs());
        }

        /// <summary>
        /// 指定された TItem 配列の要素すべてを, DuplicateKeyedSortedList に追加します.
        /// </summary>
        /// <param name="items">追加する TItem 配列です.</param>
        /// <exception cref="System.ArgumentNullException">items が null です.</exception>
        /// <remarks>
        /// <para>各要素追加完了後に, ListModified, ItemInserted イベントが発生します.</para>
        /// </remarks>
        public void AddAll(TItem[] items)
        {
            // 引数チェック
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            foreach (TItem item in items)
            {
                TKey key = this.GetKeyForItem(item);

                // このitemのkeyの入れるべき位置インデックスを取得します.
                int insertIndex = this.GetHigherIndex(key);

                // そのインデックスに挿入します. -1 の場合は末尾
                if (insertIndex == -1)
                {
                    this.Insert(this.size, key, item, false);
                }
                else
                {
                    this.Insert(insertIndex, key, item, false);
                }
            }

            // 挿入した項目のリストを取得する.
            List<TItem> insertedItemList = new List<TItem>(items);

            // イベント発生
            this.OnListModified(new EventArgs());
        }

        /// <summary>
        /// <para>指定されたインデックス位置の要素の直前に, 指定された値を挿入します.</para>
        /// <para>キーは, 指定されたインデックス位置にある要素のキーと等価のものに設定します.</para>
        /// <para>index に (最後のインデックス位置+1) が指定された場合は, 要素を末尾に追加し, キーは末尾の要素のキーと等価のものに設定します.</para>
        /// </summary>
        /// <param name="index">要素を挿入する先のインデックス位置です.</param>
        /// <param name="item">挿入する要素です.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">指定された index が, DuplicateKeyedSortedList の有効範囲外です.</exception>
        /// <exception cref="System.InvalidOperationException">DuplicateKeyedSortedList の GetKeyChangedItem が, GetKeyForItem と矛盾しています ( GetKeyChangedItem によってキーを設定した項目から, GetKeyForItem によって, 等価のキーを取得できません ).</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        public void InsertAdjustBefore(int index, TItem item)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index > this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0GreaterThan1, "index", "this._size"));
            }

            // キーを取得します. 指定されたインデックスが (最後の要素+1) でない場合は, そのインデックスのキー
            // (最後の要素+1) である場合は, 最後のインデックスのキーです.
            TKey key;
            if (index < this.size)
            {
                key = this.keys[index];
            }
            else
            {
                key = this.keys[this.size - 1];
            }

            // item に key を設定したものを取得する.
            TItem keyChangedItem = this.GetKeyChangedItem(item, key);

            // key を設定した項目のキーが key と等しいかチェック
            if (!this.ValidateKeyItemPair(key, keyChangedItem))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "this.GetKeyChangedItem", "this.GetKeyForItem"));
            }

            // キー, 値の挿入.
            this.Insert(index, key, keyChangedItem, true);
        }

        /// <summary>
        /// <para>指定されたインデックスの位置の要素の直後に, 指定された値を挿入します.</para>
        /// <para>キーは, 指定されたインデックス位置の要素のキーと等価のものに設定します.</para>
        /// </summary>
        /// <param name="index">その直後に要素を挿入する先のインデックス位置です.</param>
        /// <param name="item">挿入する要素の値です.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">指定された index が, DuplicateKeyedSortedList の有効範囲外です.</exception>
        /// <exception cref="System.InvalidOperationException">DuplicateKeyedSortedList の GetKeyChangedItem が, GetKeyForItem と矛盾しています ( GetKeyChangedItem によってキーを設定した項目から, GetKeyForItem によって, 等価のキーを取得できません ).</exception>
        /// <remarks>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        public void InsertAdjustAfter(int index, TItem item)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            // キーを取得します. 指定されたインデックスのキーです.
            TKey key = this.keys[index];

            // item に key を設定したものを取得する.
            TItem keyChangedItem = this.GetKeyChangedItem(item, key);

            // key を設定した項目のキーが key と等しいかチェック
            if (!this.ValidateKeyItemPair(key, keyChangedItem))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "this.GetKeyChangedItem", "this.GetKeyForItem"));
            }

            // 挿入を行います.
            this.Insert(index + 1, key, keyChangedItem, true);
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内で最初に見つかった特定のキー, 値を持つ要素を削除します.
        /// </summary>
        /// <param name="item">削除する要素です.</param>
        /// <returns>削除が行われた場合 true , 行われなかった場合 false です.</returns>
        /// <exception cref="System.ArgumentException">item から取得したキーが null です.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemRemoved イベントが発生します.</remarks>
        public bool Remove(TItem item)
        {
            // 引数チェック
            if (this.GetKeyForItem(item) == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            // 削除対象のインデックスを, 二分探索で取得します.
            // 削除対象のインデックスが取得された場合, 削除し, フラグをONに設定. そうでない場合はOFF.
            int index = this.IndexOf(item);
            bool removed;
            if (index >= 0)
            {
                this.RemoveAt(index);
                removed = true;
            }
            else
            {
                removed = false;
            }

            // 削除が行われたかを示す値を返します.
            return removed;
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内で指定されたキーを持つ要素のうちインデックス位置が最も小さいものを削除します.
        /// </summary>
        /// <param name="key">削除する要素のキーです.</param>
        /// <returns>削除が行われた場合 true, 行われなかった場合 false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemRemoved イベントが発生します.</remarks>
        public bool RemoveByKeyFirst(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 削除対象のインデックスを, 二分探索で取得します.
            // 削除対象のインデックスが取得された場合, 削除し, フラグをONに設定. そうでない場合はOFF
            int index = this.IndexOfKey(key);
            bool removed;
            if (index >= 0)
            {
                this.RemoveAt(index);
                removed = true;
            }
            else
            {
                removed = false;
            }

            // 削除が行われたかを示す値を返します.
            return removed;
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内で指定されたキーを持つ要素のうちインデックス位置が最も大きいものを削除します.
        /// </summary>
        /// <param name="key">削除する要素のキーです.</param>
        /// <returns>削除が行われた場合 true, 行われなかった場合 false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemRemoved イベントが発生します.</remarks>
        public bool RemoveByKeyLast(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 削除対象のインデックスを, 二分探索で取得します.
            // 削除対象のインデックスが取得された場合, 削除し, フラグをONに設定. そうでない場合はOFF
            int index = this.LastIndexOfKey(key);
            bool removed;
            if (index >= 0)
            {
                this.RemoveAt(index);
                removed = true;
            }
            else
            {
                removed = false;
            }

            // 削除が行われたかを示す値を返します.
            return removed;
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内で指定されたキーを持つ要素をすべて削除します.
        /// </summary>
        /// <param name="key">削除する要素のキーです.</param>
        /// <returns>削除した要素の数です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemMultiRemoved イベントが発生します. 個々の削除要素に対する ItemRemoved イベントは発生しません.</remarks>
        public int RemoveByKeyAll(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 削除対象のインデックスを, 二分探索で取得.
            ListIndexRange indexRange = this.GetIndexRangeOfKey(key);
            int index = indexRange.Index;
            int count = indexRange.Count;

            // 削除対象のインデックスが取得された場合, 削除.
            if (index >= 0)
            {
                this.RemoveRange(index, count);
            }

            // 削除が行われたかを示す値を返します.
            return count;
        }

        /// <summary>
        /// DuplicateKeyedSortedList から指定されたキーを持つ要素を検索し, 該当する要素のうちインデックスが最も小さいものを削除します.
        /// </summary>
        /// <param name="key">削除する要素のキーです.</param>
        /// <returns>削除が行われた場合 true, 行われなかった場合 false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemRemoved イベントが発生します.</remarks>
        bool IDictionary<TKey, TItem>.Remove(TKey key)
        {
            return this.RemoveByKeyFirst(key);
        }

        /// <summary>
        /// DuplicateKeyedSortedList から指定されたキー, 値を持つ項目を検索し, 該当する要素のうちインデックスが最も小さいものを削除します.
        /// </summary>
        /// <param name="item">削除対象の項目です.</param>
        /// <returns>削除が行われた場合 true, 行われなかった場合 false です.</returns>
        /// <exception cref="System.ArgumentException">item.Key が null です. または, item.Key が, item.Value のキーと等価でありません.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemRemoved イベントが発生します.</remarks>
        bool ICollection<KeyValuePair<TKey, TItem>>.Remove(KeyValuePair<TKey, TItem> item)
        {
            // 引数チェック
            if (item.Key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0Is1, "item.Key", "null"), "item");
            }

            if (!this.ValidateKeyItemPair(item.Key, item.Value))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "item.Key", "item.Value"), "item");
            }

            // 削除対象のインデックスを, 二分探索で取得します.
            // 削除対象のインデックスが取得された場合, 削除し, フラグをONに設定. そうでない場合はOFF
            int index = this.IndexOf(item.Value);
            bool removed;
            if (index >= 0)
            {
                this.RemoveAt(index);
                removed = true;
            }
            else
            {
                removed = false;
            }

            // 削除が行われたかを示す値を返します.
            return removed;
        }

        /// <summary>
        /// IDictionary から指定されたキーを持つ要素を検索し, 該当する要素のうちインデックスが最も小さいものを削除します.
        /// </summary>
        /// <param name="key">削除する要素のキーです.</param>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemRemoved イベントが発生します.</remarks>
        void IDictionary.Remove(object key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // TKey型へのキャストを試みる. キャストできない場合, 削除せず終了します.
            TKey concreteKey;
            try
            {
                concreteKey = (TKey)key;
            }
            catch (InvalidCastException)
            {
                return;
            }

            // 削除を行います.
            this.RemoveByKeyFirst(concreteKey);
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内の指定されたインデックス位置の項目を削除します.
        /// </summary>
        /// <param name="index">削除する項目のインデックス位置です.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index が,  DuplicateKeyedSortedList の有効な範囲でありません.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemRemoved イベントが発生します.</remarks>
        public void RemoveAt(int index)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index > this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            // 削除対象要素を取得
            TItem removedItem = this.items[index];

            // 配列内容更新. 削除要素より後を, 1つ前へコピー.
            if (index != this.size - 1)
            {
                Array.Copy(this.keys, index + 1, this.keys, index, this.size - index - 1);
                Array.Copy(this.items, index + 1, this.items, index, this.size - index - 1);
            }

            // サイズが減った結果, 無効となる要素の内容をクリア.
            this.keys[this.size - 1] = default(TKey);
            this.items[this.size - 1] = default(TItem);

            // サイズを-1
            this.size--;

            // イベント発生
            this.OnListModified(new EventArgs());
        }

        /// <summary>
        /// DuplicateKeyedSortedList から指定された範囲の要素を削除します.
        /// </summary>
        /// <param name="index">削除する範囲の開始インデックス位置です.</param>
        /// <param name="count">削除する範囲の要素の数です.</param>
        /// <exception cref="System.ArgumentException">index, count が, DuplicateKeyedSortedList 内の有効な範囲でありません.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">indexが 0 未満. または count が 0 未満 です.</exception>
        /// <remarks>要素削除完了後に, ListModified, ItemMultiRemoved イベントが発生します. 個々の削除要素に対する ItemRemoved イベントは発生しません.</remarks>
        public void RemoveRange(int index, int count)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (count < 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "count", "0"), "count");
            }

            if (index + count - 1 >= this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1EqualsMoreThan3, "index", "count", String_IndexNumber, "this._size"), "index");
            }

            // 削除対象要素を取得
            TItem[] removedItems = this.ToArray(index, count);

            // 配列内容更新. 削除要素より後を, 1つ前へコピー.
            if (index != this.size - count)
            {
                Array.Copy(this.keys, index + count, this.keys, index, this.size - index - count);
                Array.Copy(this.items, index + count, this.items, index, this.size - index - count);
            }

            // サイズが減った結果, 無効となる要素の内容をクリア.
            Array.Clear(this.keys, this.size - count, count);
            Array.Clear(this.items, this.size - count, count);

            // サイズを削除した要素数の分だけ減らす
            this.size -= count;

            // イベント発生
            this.OnListModified(new EventArgs());
        }

        /// <summary>
        /// DuplicateKeyedSortedList からすべての要素を削除します.
        /// </summary>
        /// <remarks>要素削除完了後に, ListModified, ItemMultiRemoved イベントが発生します. 個々の削除要素に対する ItemRemoved イベントは発生しません.</remarks>
        public void Clear()
        {
            if (this.size > 0)
            {
                // 結果的に削除される要素を取得します.
                IList<TItem> removedItems = this.ToArray();

                // 配列のすべての要素を削除
                Array.Clear(this.keys, 0, this.size);
                Array.Clear(this.items, 0, this.size);
                this.size = 0;

                // イベント発生
                this.OnListModified(new EventArgs());
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内のすべての範囲を互換性のある 1 次元の配列にコピーします.
        /// </summary>
        /// <param name="array">コピー先の配列です.</param>
        /// <exception cref="System.ArgumentException">array の要素数が, コピー元 DuplicateKeyedSortedList の要素数未満です.</exception>
        /// <exception cref="System.ArgumentNullException">array が null です.</exception>
        public void CopyTo(TItem[] array)
        {
            // 引数チェック
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Length < this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "array.Length", "this._size"), "array");
            }

            // 全要素を配列に格納していく.
            Array.Copy(this.items, array, this.size);
        }

        /// <summary>
        /// ICollection 内の指定された範囲を互換性のある 1 次元の配列にコピーします.
        /// </summary>
        /// <param name="array">コピー先の配列です.</param>
        /// <param name="arrayIndex">array のコピー先範囲の開始インデックス位置です.</param>
        /// <exception cref="System.ArgumentException">array のコピー先範囲の要素数が, コピー元 ICollection の要素数未満です.</exception>
        /// <exception cref="System.ArgumentNullException">array が null です.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex が 0 未満です.</exception>
        void ICollection<TItem>.CopyTo(TItem[] array, int arrayIndex)
        {
            // 引数チェック
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "arrayIndex", "0"));
            }

            if (arrayIndex > array.Length)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0GreaterThan1, "arrayIndex", "array.Length"), "array");
            }

            if (array.Length - arrayIndex < this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1LessThan3, "array", "arrayIndex", String_ItemCount, "this._size"));
            }

            // 各要素について, 配列に TItem を格納していく.
            Array.Copy(this.items, arrayIndex, array, 0, this.size - arrayIndex);
        }

        /// <summary>
        /// ICollection 内の指定された範囲を互換性のある 1 次元の配列にコピーします.
        /// </summary>
        /// <param name="array">コピー先の配列です.</param>
        /// <param name="arrayIndex">array のコピー先範囲の開始インデックス位置です.</param>
        /// <exception cref="System.ArgumentException">array のコピー先範囲の要素数が, コピー元 ICollection の要素数未満です.</exception>
        /// <exception cref="System.ArgumentNullException">array が null です.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex が 0 未満です.</exception>
        void ICollection<KeyValuePair<TKey, TItem>>.CopyTo(KeyValuePair<TKey, TItem>[] array, int arrayIndex)
        {
            // 引数チェック
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "arrayIndex", "0"));
            }

            if (arrayIndex > array.Length)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0GreaterThan1, "arrayIndex", "array.Length"), "array");
            }

            if (array.Length - arrayIndex < this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1LessThan3, "array", "arrayIndex", String_ItemCount, "this._size"));
            }

            // 各要素について, 配列に KeyValuePair を格納していく.
            for (int i = 0; i < this.size; i++)
            {
                TKey key = this.keys[i];
                TItem item = this.items[i];
                KeyValuePair<TKey, TItem> keyValuePair = new KeyValuePair<TKey, TItem>(key, item);
                array.SetValue(keyValuePair, i + arrayIndex);
            }
        }

        /// <summary>
        /// ICollection 内の指定された範囲を配列にコピーします.
        /// </summary>
        /// <param name="array">コピー先の配列です.</param>
        /// <param name="arrayIndex">コピー先の範囲の開始インデックスです.</param>
        /// <exception cref="System.ArgumentException">
        /// <para>array が多次元です. arrayIndex が array の長さを超えています. コピー元の要素数が, コピー先の格納できる数を超えています.</para>
        /// <para>または, コピー元の型が, コピー先の型に自動的にキャストできません.</para>
        /// </exception>
        /// <exception cref="System.ArgumentNullException">array が null です.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex が 0 未満です.</exception>
        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            // 引数チェック
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "arrayIndex", "0"));
            }

            if (array.Rank > 1)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0Is1, "array", String_MultiDimension), "array");
            }

            if (arrayIndex > array.Length)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0GreaterThan1, "arrayIndex", "array.Length"), "array");
            }

            if (array.Length - arrayIndex < this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1LessThan3, "array", "arrayIndex", String_ItemCount, "this._size"));
            }

            // 格納先配列要素の型にキャストできるかを調べる.
            // つまり, 格納先配列要素のクラスと同一クラスであるか, そのサブクラスであるなら良い.
            // そうでない場合は例外
            Type typeListItem = typeof(KeyValuePair<TKey, TItem>);
            Type typeArrayItem = array.GetType().GetElementType();
            if (!typeListItem.Equals(typeArrayItem) &&
                !typeListItem.IsSubclassOf(typeArrayItem))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0HasTypeInvalid, "array"), "array");
            }

            // 各要素について, 配列に値を格納していく.
            for (int i = 0; i < this.size; i++)
            {
                TKey key = this.keys[i];
                TItem item = this.items[i];
                array.SetValue(item, i + arrayIndex);
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内の指定された範囲を互換性のある 1 次元の配列にコピーします.
        /// </summary>
        /// <param name="index">DuplicateKeyedSortedList のコピー元範囲の開始インデックス位置.</param>
        /// <param name="array">コピー先の配列です.</param>
        /// <param name="arrayIndex">array のコピー先範囲の開始インデックス位置.</param>
        /// <param name="count">コピーする要素の数です.</param>
        /// <exception cref="System.ArgumentException">
        /// <para>arrayIndex が array の範囲外です. または, arrayIndex, count の組合せが, array の範囲外です.</para>
        /// <para>または, index, count の組合せが, DuplicateKeyedSortedList の範囲外です.</para>
        /// </exception>
        /// <exception cref="System.ArgumentNullException">array が null です.</exception>
        public void CopyTo(int index, TItem[] array, int arrayIndex, int count)
        {
            // 引数チェック
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "arrayIndex", "0"));
            }

            if (arrayIndex > array.Length)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0GreaterThan1, "arrayIndex", "array.Length"), "array");
            }

            if (count < 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "count", "0"), "count");
            }

            if (arrayIndex + count - 1 >= array.Length)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1EqualsMoreThan3, "arrayIndex", "count", String_IndexNumber, "array.Length"), "arrayIndex");
            }

            if (index + count - 1 >= this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1EqualsMoreThan3, "index", "count", String_IndexNumber, "this._size"), "index");
            }

            // 各要素について, 配列に格納していく.
            Array.Copy(this.items, index, array, arrayIndex, count);
        }

        /// <summary>
        /// DuplicateKeyedSortedList の要素を新しい配列にコピーします.
        /// </summary>
        /// <returns>DuplicateKeyedSortedList の要素のコピーを格納する配列です.</returns>
        public TItem[] ToArray()
        {
            TItem[] array = new TItem[this.size];
            this.CopyTo(array);
            return array;
        }

        /// <summary>
        /// DuplicateKeyedSortedList の値リストの要素を新しい値配列にコピーします.
        /// </summary>
        /// <param name="index">範囲の開始インデックスです.</param>
        /// <param name="count">範囲の要素数です.</param>
        /// <returns>DuplicateKeyedSortedList の要素のコピーを格納する配列です.</returns>
        /// <exception cref="System.ArgumentException">index, count が範囲外です.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index が 0 未満, または count が 0 未満です.</exception>
        public TItem[] ToArray(int index, int count)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "count", "0"));
            }

            if (index + count - 1 >= this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1EqualsMoreThan3, "index", "count", String_IndexNumber, "this._size"), "index");
            }

            // 配列を生成.
            TItem[] newValues = new TItem[count];
            Array.Copy(this.items, index, newValues, 0, count);
            return newValues;
        }

        /// <summary>
        /// DuplicateKeyedSortedList のキーリストの要素を新しいキー配列にコピーします.
        /// </summary>
        /// <returns>DuplicateKeyedSortedList の要素のコピーを格納する配列です.</returns>
        public TKey[] ToKeyArray()
        {
            TKey[] newKeys = new TKey[this.size];
            Array.Copy(this.keys, newKeys, this.size);
            return newKeys;
        }

        /// <summary>
        /// DuplicateKeyedSortedList のキーリストの要素を新しいキー配列にコピーします.
        /// </summary>
        /// <param name="index">キー配列にコピーする範囲の開始インデックスです.</param>
        /// <param name="count">キー配列にコピーする範囲の要素数です.</param>
        /// <returns>DuplicateKeyedSortedList の要素のコピーを格納する配列です.</returns>
        /// <exception cref="System.ArgumentException">index, count が範囲外です.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index が 0 未満, または count が 0 未満です.</exception>
        public TKey[] ToKeyArray(int index, int count)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "count", "0"));
            }

            if (index + count - 1 >= this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1EqualsMoreThan3, "index", "count", String_IndexNumber, "this._size"), "index");
            }

            // 配列を生成.
            TKey[] newKeys = new TKey[count];
            Array.Copy(this.keys, index, newKeys, 0, count);
            return newKeys;
        }

        /// <summary>
        /// DuplicateKeyedSortedList の要素を反復処理する列挙子を返します.
        /// </summary>
        /// <returns>DuplicateKeyedSortedList の要素を反復処理する列挙子です.</returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            // Enumeratorを作成して返します.
            return new DuplicateKeyedSortedList<TKey, TItem>.ValueEnumerator(this);
        }

        /// <summary>
        /// DuplicateKeyedSortedList の要素を反復処理する列挙子を返します.
        /// </summary>
        /// <returns>DuplicateKeyedSortedList の要素を反復処理する列挙子です.</returns>
        IEnumerator<KeyValuePair<TKey, TItem>> IEnumerable<KeyValuePair<TKey, TItem>>.GetEnumerator()
        {
            // Enumeratorを作成して返します.
            return new DuplicateKeyedSortedList<TKey, TItem>.KeyValuePairEnumerator(this);
        }

        /// <summary>
        /// IEnumerator の要素を反復処理する列挙子 IEnumerator を返します.
        /// </summary>
        /// <returns>DuplicateKeyedSortedList の要素を反復処理する列挙子です.</returns>
        /// <remarks>IList&gt;TItem&lt; の親インターフェースの操作を提供します.  IDictionary&lt;TKey, TItem&gt; の親インターフェースの操作は提供されないことに注意してください.</remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// IDictionaryEnumerator の要素を反復処理する列挙子 IDictionaryEnumerator を返します.
        /// </summary>
        /// <returns>DuplicateKeyedSortedList の要素を反復処理する列挙子です.</returns>
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            // DictionaryEnumeratorを作成して返します.
            return new DuplicateKeyedSortedList<TKey, TItem>.DictionaryEntryEnumerator(this);
        }

        /// <summary>
        /// DuplicateKeyedSortedList に指定された要素が格納されているかを返します.
        /// </summary>
        /// <param name="item">格納されているか調べる要素です.</param>
        /// <returns>DuplicateKeyedSortedList に指定された項目が格納されている場合, true. そうでない場合, false です.</returns>
        /// <exception cref="System.ArgumentException">item から取得したキーが null です.</exception>
        public bool Contains(TItem item)
        {
            // 引数チェック
            if (this.GetKeyForItem(item) == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            // インデックスの取得が行えるかを判定することにより, 格納されているか判断する.
            return this.IndexOf(item) != -1;
        }

        /// <summary>
        /// DuplicateKeyedSortedList に指定されたキーが格納されているかを返します.
        /// </summary>
        /// <param name="key">格納されているか調べるキーです.</param>
        /// <returns>DuplicateKeyedSortedList に指定された項目が格納されている場合, true. そうでない場合, false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        bool IDictionary.Contains(object key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // TKeyにキャスト. キャストできない場合, 含まれていないので false を返す.
            TKey tkey;
            try
            {
                tkey = (TKey)key;
            }
            catch (InvalidCastException)
            {
                return false;
            }

            // 格納されているかを返す
            return this.GetFirstFoundIndexOfKey(tkey) >= 0;
        }

        /// <summary>
        /// DuplicateKeyedSortedList に指定されたキー, 値を持つ項目が格納されているかを返します.
        /// </summary>
        /// <param name="item">格納されているか調べる項目です.</param>
        /// <returns>DuplicateKeyedSortedList に指定した項目が格納されている場合, true. そうでない場合, false です.</returns>
        /// <exception cref="System.ArgumentException">item.Key が null です. または, item.Key が, item.Value のキーと等価でありません.</exception>
        bool ICollection<KeyValuePair<TKey, TItem>>.Contains(KeyValuePair<TKey, TItem> item)
        {
            // 引数チェック
            if (item.Key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0Is1, "item.Key", "null"), "item");
            }

            if (!this.ValidateKeyItemPair(item.Key, item.Value))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0And1NotConsistent, "item.Key", "item.Value"), "item");
            }

            // 指定されたキー, 値を持つ項目のインデックスを取得し, -1以外が返されれば, 見つかったと判断し, trueを返します.
            return this.IndexOf(item.Value) != -1;
        }

        /// <summary>
        /// DuplicateKeyedSortedList に指定されたキーが格納されているかを返します.
        /// </summary>
        /// <param name="key">格納されているか調べるキーです.</param>
        /// <returns>DuplicateKeyedSortedList に指定されたキーが格納されている場合, true. そうでない場合, false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public bool ContainsKey(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーのインデックス位置を取得した際, 正であれば, 格納されている.
            return this.GetFirstFoundIndexOfKey(key) >= 0;
        }

        /// <summary>
        /// 指定されたキーを持つインデックス範囲を取得します.
        /// </summary>
        /// <param name="key">インデックス範囲を取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーを持つインデックス範囲です.</para>
        /// <para>ListIndexRange のプロパティ Index は, 指定されたキーを持つ最初のインデックス位置です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList 内の1つ以上の要素より小さい場合は, keyより大きい最初の要素のインデックス位置のビットごと補数（負の値）です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList 内のどの要素より小さい場合は, (最後の要素のインデックス位置 + 1) のビットごと補数（負の値）です.</para>
        /// <para>Count は, インデックス範囲の要素数です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public ListIndexRange GetIndexRangeOfKey(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得します.
            int foundIndex = this.GetFirstFoundIndexOfKey(key);

            // 返却するインデックス範囲を設定.
            // キーが一致するオブジェクトが取得された場合, 等価のキーを持つ最小, 最大インデックスを取得.
            // 取得されない場合は, BinarySearchByKeyで取得された負数を設定し, 要素数は 0 とする.
            int index;
            int count;
            if (foundIndex >= 0)
            {
                int firstIndex = this.IndexOfKeyIndex(foundIndex);
                int lastIndex = this.LastIndexOfKeyIndex(foundIndex);
                index = firstIndex;
                count = lastIndex - firstIndex + 1;
            }
            else
            {
                index = foundIndex;
                count = 0;
            }

            // インデックス範囲を返却
            return new ListIndexRange(index, count);
        }

        /// <summary>
        /// 指定されたキーに関連付けられている項目を取得します.
        /// </summary>
        /// <param name="key">項目を取得するキーです.</param>
        /// <param name="item">キーが見つかった場合は, そのキーを持つ要素のうちインデックス位置が最も小さいものです. それ以外の場合は, TItem の既定値です.</param>
        /// <returns>指定されたキーを持つ要素が格納されている場合は true , そうでない場合は false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public bool TryGetItem(TKey key, out TItem item)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // キーのインデックスを取得します.
            // キーが見つかった場合, 見つかったインデックスの値を, 取得される値として設定. フラグをON.
            // 見つからない場合は, TItem の既定値を, 取得される値として設定. フラグをOFF.
            bool tryGet;
            int index = this.IndexOfKey(key);
            if (index >= 0)
            {
                item = this.items[index];
                tryGet = true;
            }
            else
            {
                item = default(TItem);
                tryGet = false;
            }

            // キーが見つかったかを示す値を返します.
            return tryGet;
        }

        /// <summary>
        /// 指定されたキーに関連付けられている項目を取得します.
        /// </summary>
        /// <param name="key">項目を取得するキーです.</param>
        /// <param name="value">キーが見つかった場合は, そのキーを持つ要素のうちインデックス位置が最も小さいものです. それ以外の場合は, TItem の既定値です.</param>
        /// <returns>指定されたキーを持つ要素が格納されている場合は true , そうでない場合は false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        bool IDictionary<TKey, TItem>.TryGetValue(TKey key, out TItem value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            return this.TryGetItem(key, out value);
        }

        /// <summary>
        /// 指定されたインデックス位置にある値を取得します.
        /// </summary>
        /// <param name="index">値を取得するインデックス位置です.</param>
        /// <returns>指定されたインデックス位置にある値です.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index が有効範囲外です.</exception>
        public TItem GetByIndex(int index)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            return this.items[index];
        }

        /// <summary>
        /// 指定されたインデックス位置にあるキーを取得します.
        /// </summary>
        /// <param name="index">キーを取得するインデックス位置.</param>
        /// <returns>指定されたインデックス位置にあるキーです.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index が有効範囲外です.</exception>
        public TKey GetKey(int index)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            return this.keys[index];
        }

        /// <summary>
        /// 指定されたインデックス位置に指定された項目を設定します.
        /// </summary>
        /// <param name="index">値を設定するインデックス位置です.</param>
        /// <param name="item">設定する値です.</param>
        /// <exception cref="System.ArgumentException">item のキーが, 現在そのインデックスに設定されている項目のキーと一致していません.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index が有効範囲外です.</exception>
        /// <remarks>指定された item のキーは, 現在そのインデックスに設定されている項目のキーと一致している必要があります.</remarks>
        public void SetByIndex(int index, TItem item)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            if (!this.ValidateKeyItemPair(this.GetKey(index), item))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0And2NotConsistent, "index", String_Key, "item"), "index");
            }

            this.items[index] = item;
        }

        /// <summary>
        /// 指定されたインデックス位置にある項目に, 指定されたキーを設定します. 設定後, 要素をリスト内の適切な位置に移動します.
        /// </summary>
        /// <param name="index">キーを設定するインデックス位置です.</param>
        /// <param name="key">設定するキーです.</param>
        /// <returns>キーを設定した要素の, 移動先のインデックス位置です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>処理完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        public int SetKeyByIndex(int index, TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            // キー設定, 並べ替え
            TItem item = this.GetByIndex(index);
            this.RemoveAtWithoutEvent(index);
            TItem keyChangedItem = this.GetKeyChangedItem(item, key);
            int newIndex = this.Add(keyChangedItem);

            // 移動先インデックスを返します.
            return newIndex;
        }

        /// <summary>
        /// <para>指定された項目を DuplicateKeyedSortedList から検索し, 見つかった場合に指定されたキーを設定します. 設定後, 要素をリスト内の適切な位置に移動します.</para>
        /// <para>見つからない場合は何も行いません.</para>
        /// </summary>
        /// <param name="item">リストから検索してキーを設定する項目.</param>
        /// <param name="key">設定するキーです.</param>
        /// <returns>キーを設定した要素の, 移動先のインデックス位置です. 指定された項目が DuplicateKeyedSortedList に見つからない場合は, -1 です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <remarks>指定された項目が見つかった場合は, 処理完了後に, ListModified, ItemInserted イベントが発生します.</remarks>
        public int SetKeyByIndexOf(TItem item, TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // インデックス位置取得. 見つからない場合は -1 を返して終了
            int index = this.IndexOf(item);
            if (index < 0 || index >= this.Count)
            {
                return -1;
            }

            // キー設定, 並べ替え
            this.RemoveAtWithoutEvent(index);
            TItem keyChangedItem = this.GetKeyChangedItem(item, key);
            int newIndex = this.Add(keyChangedItem);

            // 移動先インデックスを返します.
            return newIndex;
        }

        /// <summary>
        /// 指定されたキーに設定してある最初のオブジェクトのインデックスを, 二分探索で取得します.
        /// </summary>
        /// <param name="key">インデックスを取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーに設定してある最初のオブジェクトのインデックス位置です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList 内の1つ以上の要素より小さい場合は, key より大きい最初の要素のインデックスのビットごと補数（負の値）.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList 内のどの要素より小さい場合は, (最後の要素のインデックス + 1) のビットごと補数（負の値）.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int IndexOfKey(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得.
            // 取得された場合, 同一キー内最小インデックスを返す. そうでない場合, BinarySearchByKey による負数を返す.
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                return this.IndexOfKeyIndex(idx);
            }
            else
            {
                return idx;
            }
        }

        /// <summary>
        /// 指定されたキーに設定してある最後のオブジェクトのインデックスを, 二分探索で取得します.
        /// </summary>
        /// <param name="key">インデックスを取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーに設定してある最後のオブジェクトのインデックス位置です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList内の1つ以上の要素より小さい場合は, keyより大きい最初の要素のインデックスのビットごと補数（負の値）です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList内のどの要素より小さい場合は, 最後の要素のインデックス+1 のビットごと補数（負の値）です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int LastIndexOfKey(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得.
            // 取得された場合, 同一キー内最大インデックスを返す. そうでない場合, BinarySearchByKey による負数を返す.
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                return this.LastIndexOfKeyIndex(idx);
            }
            else
            {
                return idx;
            }
        }

        /// <summary>
        /// 指定された項目を持つ最初のインデックスを返します.
        /// </summary>
        /// <param name="item">最初のインデックスを調べる項目です.</param>
        /// <returns>DuplicateKeyedSortedList に指定した項目が格納されている場合, そのうち最初の項目のインデックス位置です. そうでない場合, -1 です.</returns>
        /// <exception cref="System.ArgumentException">item から取得したキーが null です.</exception>
        public int IndexOf(TItem item)
        {
            // 引数チェック
            TKey key = this.GetKeyForItem(item);
            if (key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            // 指定されたキーを持つ最初のインデックスを取得. 見つからない場合は -1 を返す.
            int firstIndex = this.IndexOfKey(key);
            if (firstIndex < 0)
            {
                return -1;
            }

            // 同一キーを持つ項目内で昇順に, 指定された値を持つものを検索. 見つからない場合は -1 を返す.
            IEqualityComparer<TItem> itemComparer = EqualityComparer<TItem>.Default;
            for (int i = firstIndex; i < this.size; i++)
            {
                // キーが指定されたキーでなくなる場合は, ループを抜ける.
                if (this.comparer.Compare(this.keys[i], key) != 0)
                {
                    break;
                }

                // 現在調べているインデックスの値が, 指定された値と等しければ, 見つかったので, そのインデックスを返す.
                if (itemComparer.Equals(this.items[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 指定された項目を持つ最後のインデックスを返します.
        /// </summary>
        /// <param name="item">最後のインデックスを調べる項目です.</param>
        /// <returns>DuplicateKeyedSortedList に指定した項目が格納されている場合, そのうち最後の項目のインデックス位置です. そうでない場合, -1 です.</returns>
        /// <exception cref="System.ArgumentException">item から取得したキーが null です.</exception>
        public int LastIndexOf(TItem item)
        {
            // 引数チェック
            TKey key = this.GetKeyForItem(item);
            if (key == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_1From0Is2, "item", String_Key, "null"), "item");
            }

            // 指定されたキーを持つ最後のインデックスを取得. 見つからない場合は -1 を返す.
            int lastIndex = this.LastIndexOfKey(key);
            if (lastIndex < 0)
            {
                return -1;
            }

            // 同一キーを持つ項目内で降順に, 指定された値を持つものを検索. 見つからない場合は -1 を返す.
            IEqualityComparer<TItem> itemComparer = EqualityComparer<TItem>.Default;
            for (int i = lastIndex; i >= 0; i--)
            {
                // キーが指定されたキーでなくなる場合は, ループを抜ける.
                if (this.comparer.Compare(this.keys[i], key) != 0)
                {
                    break;
                }

                // 現在調べているインデックスの値が, 指定された値と等しければ, 見つかったので, そのインデックスを返す.
                if (itemComparer.Equals(this.items[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 指定されたインデックスのオブジェクトと等しいキーを持つオブジェクトのうち, インデックスが最小のオブジェクトのインデックスを取得します.
        /// </summary>
        /// <param name="index">オブジェクトと等しいキーを持つ要素のうち, 最小のインデックスを取得する, インデックス位置です.</param>
        /// <returns>
        /// <para>オブジェクトと等しいキーを持つオブジェクトのうち, インデックスが最小のオブジェクトのインデックス位置です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">インデックスが DuplicateKeyedSortedList の有効なインデックスでありません.</exception>
        public int IndexOfKeyIndex(int index)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            // キーが等しい値の中で, インデックスが最小のもののインデックスを取得.
            // 開始インデックスからインデックスを小さくしていき, 次のインデックスの項目のキーが現在と異なっていたら, そのインデックスを返す.
            // 0 まで等しい場合は, 0 を最小インデックスとして返す.
            for (int i = index; i > 0; i--)
            {
                TKey nowKey = this.keys[i];
                TKey nextKey = this.keys[i - 1];
                int result = this.comparer.Compare(nextKey, nowKey);
                if (result < 0)
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// 指定されたインデックス位置のオブジェクトと等しいキーを持つオブジェクトのうち, インデックスが最大のオブジェクトのインデックス位置を取得します.
        /// </summary>
        /// <param name="index">オブジェクトと等しいキーを持つ要素のうち, 最大のインデックスを取得する, インデックス位置です.</param>
        /// <returns>
        /// <para>オブジェクトと等しいキーを持つオブジェクトのうち, インデックスが最大のオブジェクトのインデックス位置です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index が DuplicateKeyedSortedList の有効なインデックスでありません.</exception>
        public int LastIndexOfKeyIndex(int index)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0EqualsGreaterThan1, "index", "this._size"));
            }

            // キーが等しい値の中で, インデックスが最大のもののインデックスを取得.
            // 開始インデックスからインデックスを大きくしていき, 次のインデックスの項目のキーが現在と異なっていたら, そのインデックスを返す.
            // DuplicateKeyedSortedList の最終インデックスまで等しい場合は, それを最大インデックスとして返す.
            for (int i = index; i < this.size - 1; i++)
            {
                // 現在のインデックスのキー
                TKey nowKey = this.keys[i];

                // 次のインデックスのキー
                TKey nextKey = this.keys[i + 1];

                // 現在と次のインデックスのキーを比較します.
                int result = this.comparer.Compare(nextKey, nowKey);

                // 次のインデックスのキーが, 現在のものと異なる（現在より大きい）場合
                if (result > 0)
                {
                    // 現在のインデックスを, 最大インデックスとして返します.
                    return i;
                }
            }

            return this.size - 1;
        }

        /// <summary>
        /// 指定されたキーより小さいキーの中で最大のキーを取得します.
        /// </summary>
        /// <param name="key">それより小さいキーの中で最大のものを取得するキーです.</param>
        /// <param name="lowerKey">指定されたキーより小さいキーの中で最大のものです. ない場合はデフォルト値です.</param>
        /// <returns>指定されたキーより小さいキーの中で最大のキーが取得できた場合は true, それ以外の場合は false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public bool GetLower(TKey key, out TKey lowerKey)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーより小さいキーを持つインデックスの中で, 最大のインデックスを取得, そのキーを返す.
            // 取得できない場合は, TKey のデフォルト値を返す.
            int idx = this.GetLowerIndex(key);
            if (idx != -1)
            {
                lowerKey = this.keys[idx];
                return true;
            }
            else
            {
                lowerKey = default(TKey);
                return false;
            }
        }

        /// <summary>
        /// 指定されたキーより大きいキーの中で最小のキーを取得します.
        /// </summary>
        /// <param name="key">それより大きいキーの中で最小のものを取得するキーです.</param>
        /// <param name="higherKey">指定されたキーより大きいキーの中で最小のものです. ない場合はデフォルト値です.</param>
        /// <returns>指定されたキーより大きいキーの中で最小のキーが取得できた場合は true, それ以外の場合は false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public bool GetHigher(TKey key, out TKey higherKey)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーより大きいキーを持つインデックスの中で, 最小のインデックスを取得, そのキーを返す.
            // 取得できない場合は, TKey のデフォルト値を返す.
            int idx = this.GetHigherIndex(key);
            if (idx != -1)
            {
                higherKey = this.keys[idx];
                return true;
            }
            else
            {
                higherKey = default(TKey);
                return false;
            }
        }

        /// <summary>
        /// 指定されたキーと等しいか, それより小さいキーの中で最大のキーを取得します.
        /// </summary>
        /// <param name="key">それと等しいか, それより小さいキーの中で最大のものを取得するキーです.</param>
        /// <param name="floorKey">指定されたキーと等しいか, それより小さいキーの中で最大のものです. ない場合はデフォルト値です.</param>
        /// <returns>指定されたキーと等しいか, それより小さいキーの中で最大のキーが取得できた場合は true, それ以外の場合は false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public bool GetFloor(TKey key, out TKey floorKey)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得, キーを返す.
            // 該当キーが存在せず, その次のインデックスの補数が取得された場合は, そのインデックスから取得.
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                floorKey = this.keys[idx];
                return true;
            }
            else
            {
                // 取得された数値は, 取得インデックスの補数であるので, 元の数に戻す.
                idx = ~idx;

                // インデックスが0以外の場合, 前のインデックスのキー, 0 の場合, TKey のデフォルト値を返す.
                if (idx > 0)
                {
                    floorKey = this.keys[idx - 1];
                    return true;
                }
                else
                {
                    floorKey = default(TKey);
                    return false;
                }
            }
        }

        /// <summary>
        /// 指定されたキーと等しいか, それより大きいキーの中で最小のキーを取得します.
        /// </summary>
        /// <param name="key">それと等しいか, それより大きいキーの中で最小のものを取得するキーです.</param>
        /// <param name="ceilingKey">指定されたキーと等しいか, それより大きいキーの中で最小のものです. ない場合はデフォルト値です.</param>
        /// <returns>指定されたキーと等しいか, それより大きいキーの中で最小のキーが取得できた場合は true, それ以外の場合は false です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public bool GetCeiling(TKey key, out TKey ceilingKey)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得, キーを返す.
            // 該当キーが存在せず, その次のインデックスの補数が取得された場合は, そのインデックスから取得
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                // そのインデックスのキーを返します.
                ceilingKey = this.keys[idx];
                return true;
            }
            else
            {
                // 取得された数値は, 取得インデックスの補数であるので, 元の数に戻す.
                idx = ~idx;

                // インデックスがリスト範囲内の場合, そのインデックスのキー, 0 の場合, TKey のデフォルト値を返す.
                if (idx < this.size)
                {
                    ceilingKey = this.keys[idx];
                    return true;
                }
                else
                {
                    ceilingKey = default(TKey);
                    return false;
                }
            }
        }

        /// <summary>
        /// 指定されたキーより小さいキーを持つインデックスの中で, 最大のインデックスを返します.
        /// </summary>
        /// <param name="key">それより小さいキーの中で最大のものを取得するキーです.</param>
        /// <returns>指定されたキーより小さいキーを持つインデックスの中で最大のもの. ない場合は-1 です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int GetLowerIndex(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーの最小インデックスを, 二分探索法で取得, 一致キーインデックスが取得された場合は1つ前のインデックスを返す.
            // 次のインデックスの補数が取得された場合は, その前のインデックスを返す.
            // 該当インデックスが DuplicateKeyedSortedList の範囲外の場合は, -1 を返す.
            int idx = this.IndexOfKey(key);
            if (idx >= 0)
            {
                if (idx > 0)
                {
                    return idx - 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                idx = ~idx;
                if (idx > 0)
                {
                    return idx - 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 指定されたキーより大きいキーを持つインデックスの中で, 最小のインデックスを返します.
        /// </summary>
        /// <param name="key">それより大きいキーの中で最小のものを取得するキーです.</param>
        /// <returns>指定されたキーより大きいキーを持つインデックスの中で最小のもの. ない場合は-1 です.</returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int GetHigherIndex(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーの最大インデックスを, 二分探索法で取得, 一致キーインデックスが取得された場合は1つ後のインデックスを返す.
            // 次のインデックスの補数が取得された場合は, そのインデックスを返す.
            // 該当インデックスが DuplicateKeyedSortedList の範囲外の場合は, -1 を返す.
            int idx = this.LastIndexOfKey(key);
            if (idx >= 0)
            {
                if (idx < this.size)
                {
                    return idx + 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                idx = ~idx;
                if (idx < this.size)
                {
                    return idx;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 指定されたキーと等しいか, それより小さいキーを持つインデックスの中で, 最大のインデックスを返します.
        /// </summary>
        /// <param name="key">それと等しいか, それより小さいキーを持つインデックスの中で, 最大のものを取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーと等しいか, それより小さいキーを持つインデックスの中で, キーが最大のインデックス位置です.</para>
        /// <para>該当するインデックスが複数ある場合は, インデックスが最大のものです.</para>
        /// <para>該当するインデックスがない場合は, -1 です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int GetFloorLastIndex(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得.
            // そのキーのインデックスが取得された場合は, 同一キーを持つ最大インデックスを返す.
            // 次のインデックスの補数が取得された場合は, その前のインデックスを返す.
            // 該当インデックスがない場合は, -1 を返す.
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                return this.LastIndexOfKeyIndex(idx);
            }
            else
            {
                idx = ~idx;
                if (idx > 0)
                {
                    return idx - 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 指定されたキーと等しいか, それより大きいキーを持つインデックスの中で, 最小のインデックスを返します.
        /// </summary>
        /// <param name="key">それと等しいか, それより大きいキーを持つインデックスの中で, 最小のものを取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーと等しいか, それより大きいキーを持つインデックスの中で, キーが最小のインデックス位置です.</para>
        /// <para>該当するインデックスが複数ある場合は, インデックスが最小のものです.</para>
        /// <para>該当するインデックスがない場合は, -1 です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int GetCeilingFirstIndex(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得.
            // そのキーのインデックスが取得された場合は, 同一キーを持つ最小インデックスを返す.
            // 次のインデックスの補数が取得された場合は, そのインデックスを返す.
            // 該当インデックスがない場合は, -1 を返す.
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                return this.IndexOfKeyIndex(idx);
            }
            else
            {
                idx = ~idx;
                if (idx < this.size)
                {
                    return idx;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// <para>指定されたキーと等しいか, それより小さいキーを持つインデックスの中で, キーが最大のインデックスを返します.</para>
        /// <para>該当するインデックスが複数ある場合は, インデックスが最小のものを返します.</para>
        /// </summary>
        /// <param name="key">それと等しいか, それより小さいキーを持つインデックスの中で, キーが最大のものを取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーと等しいか, それより小さいキーを持つインデックスの中で, キーが最大のインデックス位置です.</para>
        /// <para>該当するインデックスが複数ある場合は, インデックスが最小のものです.</para>
        /// <para>該当するインデックスがない場合は, -1 です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int GetFloorFirstIndex(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得.
            // そのキーのインデックスが取得された場合は, 同一キーを持つ最小インデックスを返す.
            // 次のインデックスの補数が取得された場合は, 前のインデックスのキーと同一キーを持つ最小インデックスを返す.
            // 該当インデックスがない場合は, -1 を返す.
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                return this.IndexOfKeyIndex(idx);
            }
            else
            {
                idx = ~idx;
                if (idx > 0)
                {
                    return this.IndexOfKeyIndex(idx - 1);
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// <para>指定されたキーと等しいか, それより大きいキーを持つインデックスの中で, キーが最小のインデックスを返します.</para>
        /// <para>該当するインデックスが複数ある場合は, インデックスが最大のものを返します.</para>
        /// </summary>
        /// <param name="key">それと等しいか, それより大きいキーを持つインデックスの中で, キーが最小のものを取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーと等しいか, それより大きいキーを持つインデックスの中で, キーが最小のインデックス位置です.</para>
        /// <para>該当するインデックスが複数ある場合は, インデックスが最大のものです.</para>
        /// <para>該当するインデックスがない場合は, -1 です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        public int GetCeilingLastIndex(TKey key)
        {
            // 引数チェック
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            // 指定されたキーに設定してあるオブジェクトのインデックスを, 二分探索で取得.
            // そのキーのインデックスが取得された場合は, 同一キーを持つ最大インデックスを返す.
            // 次のインデックスの補数が取得された場合は, そのインデックスのキーと同一キーを持つ最大インデックスを返す.
            // 該当インデックスがない場合は, -1 を返す.
            int idx = this.GetFirstFoundIndexOfKey(key);
            if (idx >= 0)
            {
                return this.LastIndexOfKeyIndex(idx);
            }
            else
            {
                idx = ~idx;
                if (idx < this.size)
                {
                    return this.LastIndexOfKeyIndex(idx);
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList にある実際の要素数が現在の容量の90%値未満の場合, 容量をその数に設定します.
        /// </summary>
        public void TrimExcess()
        {
            // 閾値
            int threshold = (int)(this.keys.Length * 0.9);

            if (this.size < threshold)
            {
                this.Capacity = this.size;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList をシリアル化するために必要なデータを返します.
        /// </summary>
        /// <param name="info">DuplicateKeyedSortedList をシリアル化するために必要な情報.</param>
        /// <param name="context">DuplicateKeyedSortedList に関連付けられているシリアル化ストリームのソース, デスティネーションを格納しているオブジェクトです.</param>
        /// <exception cref="System.ArgumentNullException">info が null です.</exception>
        /// <exception cref="System.Security.SecurityException">呼び出し元に, 必要なアクセス許可がありません.</exception>
        /// <remarks>DuplicateKeyedSortedList をシリアル化するためには, メンバ _comparer が, シリアル化可能である必要があります.</remarks>
        [SecurityCritical]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // 引数チェック
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("comparer", this.comparer, typeof(IComparer<TKey>));
            TItem[] array = this.ToArray();
            info.AddValue("items", array, typeof(TItem[]));
        }

        /// <summary>
        /// 逆シリアル化が完了したときに逆シリアル化イベントによってcallbackされます.
        /// </summary>
        /// <param name="sender">逆シリアル化イベントのソース.</param>
        /// <exception cref="System.Runtime.Serialization.SerializationException">DuplicateKeyedSortedList と関連付けられている SerializationInfo が無効です. または, 逆シリアル化対象のオブジェクトデータが不正です.</exception>
        void IDeserializationCallback.OnDeserialization(object sender)
        {
            this.OnDeserialization();
        }

        /// <summary>
        /// 項目からキーを取得します. 項目が持つキーの情報を取得します.
        /// </summary>
        /// <param name="item">キーの情報を持つ項目です.</param>
        /// <returns>項目が持つキーの情報です.</returns>
        /// <remarks>このメソッドは, コレクションの内容が変化しない限り, ある item に対して常に同一のオブジェクトまたは値を返す必要があります.</remarks>
        protected abstract TKey GetKeyForItem(TItem item);

        /// <summary>
        /// 項目のキーを指定されたものに変更したものを取得します.
        /// </summary>
        /// <param name="item">項目です.</param>
        /// <param name="key">キーです.</param>
        /// <returns>項目のキーを指定されたものに変更したものです.</returns>
        protected abstract TItem GetKeyChangedItem(TItem item, TKey key);

        /// <summary>
        /// 逆シリアル化が完了したときに逆シリアル化イベントによってコールバックされます.
        /// </summary>
        /// <exception cref="System.Runtime.Serialization.SerializationException">
        /// <para>DuplicateKeyedSortedList と関連付けられている SerializationInfo が無効です.</para>
        /// <para>または, 逆シリアル化に必要なオブジェクトデータを正しく取得できません.</para>
        /// <para>または, シリアル化データにキーが null の項目が含まれています.</para>
        /// <para>または, シリアル化データ内でキーが正しくソートされていません.</para>
        /// </exception>
        /// <remarks>逆シリアル化対象のオブジェクトデータ内でキーが重複している場合, 予期せぬ例外が発生します.</remarks>
        protected void OnDeserialization()
        {
            // 引数チェック
            if (this.serializationInfo == null)
            {
                throw new SerializationException(ExceptionMessage_SerializationInfoNull);
            }

            // 比較演算子, キー取得デリゲートの取得
            this.comparer = (IComparer<TKey>)this.serializationInfo.GetValue("comparer", typeof(IComparer<TKey>));

            // データの取得. 各要素のキーが null でないかをチェックしながら行う.
            TItem[] array = (TItem[])this.serializationInfo.GetValue("items", typeof(TItem[]));
            if (array == null)
            {
                throw new SerializationException(ExceptionMessage_SerializationMissingData);
            }

            this.keys = new TKey[array.Length];
            this.items = new TItem[array.Length];
            TKey preKey = default(TKey);
            for (int i = 0; i < array.Length; i++)
            {
                TKey key = this.GetKeyForItem(array[i]);
                if (key == null)
                {
                    throw new SerializationException(ExceptionMessage_SerializationKeyNull);
                }

                // 直前キーより小さいキーである場合は例外
                if (i > 0)
                {
                    if (this.comparer.Compare(key, preKey) < 0)
                    {
                        throw new SerializationException(ExceptionMessage_SerializationKeyMissSort);
                    }
                }

                this.keys[i] = key;
                this.items[i] = array[i];
                preKey = key;
            }

            this.size = array.Length;
        }

        /// <summary>
        /// <para>指定されたインデックスの位置に, 指定されたキー, 値の組を挿入します.</para>
        /// <para>key の挿入先に合わせた自動調整は行いません.</para>
        /// </summary>
        /// <param name="index">要素を挿入する先のインデックス位置です.</param>
        /// <param name="key">挿入する要素のキーです.</param>
        /// <param name="item">挿入する要素の項目です.</param>
        /// <param name="eventRaise">処理後にイベントを発生させるかを示します. 発生させる場合は true, そうでない場合は false です.</param>
        /// <remarks>
        /// <para>要素追加完了後に, ListModified, ItemInserted イベントが発生します.</para>
        /// <para>キーが項目から取得されるものと一致しているかチェックを行っていません. 呼び出す前にチェックしてください.</para>
        /// <para>引数チェックを行っていません. key, item に null を指定しないようにして下さい.</para>
        /// <para>key の挿入先に合わせた自動調整を行わないので, 必ず呼び出す前に key を調整して下さい.</para>
        /// </remarks>
        private void Insert(int index, TKey key, TItem item, bool eventRaise)
        {
            // 容量が足りない場合, 確保する.
            if (this.size + 1 > this.keys.Length)
            {
                // 現在のサイズ+1 が確保されるよう, 容量を拡大します.
                this.EnsureCapacity(this.size + 1);
            }

            // 挿入する位置以降の要素を, すべて1つ次へずらす.
            if (index < this.size)
            {
                Array.Copy(this.keys, index, this.keys, index + 1, this.size - index);
                Array.Copy(this.items, index, this.items, index + 1, this.size - index);
            }

            // 挿入先の要素を, 挿入する要素で置き換える.
            this.keys[index] = key;
            this.items[index] = item;

            // サイズを +1
            this.size++;

            // イベント発生
            if (eventRaise)
            {
                this.OnListModified(new EventArgs());
            }
        }

        /// <summary>
        /// 指定されたキーを DuplicateKeyedSortedList に挿入する際の, 挿入先インデックスを取得します.
        /// </summary>
        /// <param name="key">挿入するキーです.</param>
        /// <returns>DuplicateKeyedSortedList の既存のキーと, 指定されたキーにより計算した挿入先インデックスです.</returns>
        /// <remarks>
        /// <para>このメソッドは, GetInsertIndex(key, int.MaxValue) と等価です. キーが等価の項目がある場合, それらの末尾の直後のインデックスが取得されます.</para>
        /// <para>key の null チェックは行っていません. key に null を指定しないでください.</para>
        /// </remarks>
        private int GetInsertIndex(TKey key)
        {
            // 挿入先インデックスを取得. 取得できない場合, (末尾のインデックス+1)とする.
            int insertIndex = this.GetHigherIndex(key);
            if (insertIndex == -1)
            {
                insertIndex = this.size;
            }

            // 取得した挿入先インデックスを返す.
            return insertIndex;
        }

        /// <summary>
        /// 指定されたキーを DuplicateKeyedSortedList に指定された相対インデックスを用いて挿入する際の, 挿入先インデックスを取得します.
        /// </summary>
        /// <param name="key">挿入するキーです.</param>
        /// <param name="indexIn">
        /// <para>キーが等価の項目がある場合の, その項目の中での追加先インデックスです.</para>
        /// <para>キーが等価の項目のうち最初のもののインデックス位置を 0 とした相対インデックスです.</para>
        /// <para>キーが等価の項目の数より大きい値を指定した場合は, それらの項目の末尾の直後に挿入されます. 例外はスローされません.</para>
        /// </param>
        /// <returns>DuplicateKeyedSortedList の既存のキーと,指定されたキーと相対インデックス位置により計算した挿入先インデックスです.</returns>
        /// <remarks>key と indexIn の引数チェックは行っていません. key に null を指定しないでください. また, indexIn に 0 未満の値を指定しないでください.</remarks>
        private int GetInsertIndex(TKey key, int indexIn)
        {
            // indexIn を考慮しない挿入先インデックス位置を取得.
            // 取得できない場合(key は含まれず, 既存のどのキーより大きい), (末尾のインデックス+1)とする.
            int ceilingFirstIndex = this.GetCeilingFirstIndex(key);
            if (ceilingFirstIndex == -1)
            {
                ceilingFirstIndex = this.size;
            }

            // indexIn を考慮した挿入先インデックス位置の取得.
            // 等価のキーが既に存在する場合は, indexIn によって挿入先インデックス位置を調整する.
            int insertIndex;
            if (ceilingFirstIndex < this.size)
            {
                if (this.comparer.Compare(this.keys[ceilingFirstIndex], key) == 0)
                {
                    int ceilingLastIndex = this.LastIndexOfKeyIndex(ceilingFirstIndex);
                    insertIndex = Math.Min(ceilingFirstIndex + indexIn, ceilingLastIndex + 1);
                }
                else
                {
                    insertIndex = ceilingFirstIndex;
                }
            }
            else
            {
                insertIndex = this.size;
            }

            // 取得した挿入先インデックスを返す.
            return insertIndex;
        }

        /// <summary>
        /// <para>管理しているキー配列, 値配列の要素数を拡大し, 少なくとも指定した要素数を格納できるようにします.</para>
        /// <para>具体的には, 容量が指定した要素数未満のときに, その倍の容量を確保します.</para>
        /// </summary>
        /// <param name="minCap">少なくとも格納を可能とする要素数です.</param>
        private void EnsureCapacity(int minCap)
        {
            // 指定された容量が確保されていない場合
            if (this.keys.Length <= minCap)
            {
                // 少なくとも, 現在の容量の2倍を確保するようにします.
                // 現在の容量の2倍が, 4に満たない場合は, 2倍容量を4として計算します.
                int twiceCap = this.keys.Length * 2;
                if (twiceCap < 4)
                {
                    twiceCap = 4;
                }

                // 指定された最低容量と, 現在容量の2倍のうち, 大きい値を, 確保する容量とします.
                this.Capacity = Math.Max(minCap, twiceCap);
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList 内の指定されたインデックス位置の項目を削除します. 削除の際にイベントを発生させません.
        /// </summary>
        /// <param name="index">削除する項目のインデックス位置です.</param>
        /// <remarks>
        /// <para>要素削除完了後に, ListModified, ItemRemoved イベントを発生させません. また, 引数チェックを行いません.</para>
        /// </remarks>
        private void RemoveAtWithoutEvent(int index)
        {
            // 配列内容更新. 削除要素より後を, 1つ前へコピー.
            if (index != this.size - 1)
            {
                Array.Copy(this.keys, index + 1, this.keys, index, this.size - index - 1);
                Array.Copy(this.items, index + 1, this.items, index, this.size - index - 1);
            }

            // サイズが減った結果, 無効となる要素の内容をクリア.
            this.keys[this.size - 1] = default(TKey);
            this.items[this.size - 1] = default(TItem);

            // サイズを-1
            this.size--;
        }

        /// <summary>
        /// 指定されたキーのインデックス位置を, 二分探索で取得します.
        /// </summary>
        /// <param name="key">インデックス位置を取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーのオブジェクトの, 最初に見つかったインデックス位置です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList内の1つ以上の要素より小さい場合は, keyより大きい最初の要素のインデックス位置のビットごと補数（負の値）です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList内のどの要素より小さい場合は, (最後の要素のインデックス位置 + 1) のビットごと補数（負の値）です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        private int GetFirstFoundIndexOfKey(TKey key)
        {
            return this.GetFirstFoundIndexOfKey(0, this.size, key);
        }

        /// <summary>
        /// 指定されたキーのインデックス位置を, 二分探索で取得します.
        /// </summary>
        /// <param name="index">検索開始インデックス位置です.</param>
        /// <param name="count">検索範囲の要素数です.</param>
        /// <param name="key">インデックス位置を取得するキーです.</param>
        /// <returns>
        /// <para>指定されたキーのオブジェクトの, 最初に見つかったインデックス位置です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList内の1つ以上の要素より小さい場合は, keyより大きい最初の要素のインデックス位置のビットごと補数（負の値）です.</para>
        /// <para>指定されたキーのオブジェクトが見つからず, DuplicateKeyedSortedList内のどの要素より小さい場合は, (最後の要素のインデックス位置 + 1) のビットごと補数（負の値）です.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key が null です.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index, countが, 有効なインデックスの範囲外です.</exception>
        private int GetFirstFoundIndexOfKey(int index, int count, TKey key)
        {
            // 引数チェック
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "count", "0"));
            }

            if (index + count - 1 >= this.size)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1EqualsMoreThan3, "index", "count", String_IndexNumber, "this._size"), "index");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            int idx = -1; // 取得されるインデックス
            int idxStart = index; // 探索範囲の最初のインデックス
            int idxEnd = index + count - 1; // 探索範囲の最後のインデックス

            // すべて調べても, キーが等しいインデックスが見つからない場合は, idxStart <= idxEnd
            // ではなくなるので, そのときにループから抜けるようにします.
            while (idxStart <= idxEnd)
            {
                // インデックスを取得するキーを, 中央インデックスのキーと比較. 等しくない場合, 探索範囲を前半または後半に絞る.
                // 等しい場合, それを見つかったインデックスとして打ち切る.
                int idxMiddle = (idxStart + idxEnd) / 2;
                TKey keyMiddle = this.keys[idxMiddle];
                int result = this.comparer.Compare(key, keyMiddle);
                if (result < 0)
                {
                    idxEnd = idxMiddle - 1;
                }
                else if (result > 0)
                {
                    idxStart = idxMiddle + 1;
                }
                else
                {
                    idx = idxMiddle;
                    break;
                }
            }

            // 指定されたキーのインデックスが取得された場合, そのまま返す.
            // 取得されない場合, keyより大きい最初のインデックスのビットごと補数を返す (idxStartの補数).
            if (idx != -1)
            {
                return idx;
            }
            else
            {
                return ~idxStart;
            }
        }

        /// <summary>
        /// キー TKey, 項目 TItem の組合せが, この DuplicateKeyedSortedList の要素として適切なものであるかを検証します.
        /// </summary>
        /// <param name="key">キー TKey です.</param>
        /// <param name="item">項目 TItem です.</param>
        /// <returns>キー TKey, 項目 TItem の組合せが, この DuplicateKeyedSortedList の要素として適切なものである場合は true, そうでない場合は false です.</returns>
        /// <remarks>キーの null チェックを行っていません. key に null を指定しないでください.</remarks>
        private bool ValidateKeyItemPair(TKey key, TItem item)
        {
            TKey keyFromItem = this.GetKeyForItem(item);
            return this.comparer.Compare(key, keyFromItem) == 0;
        }

        /// <summary>
        /// ListModified イベントを発生させます.
        /// </summary>
        /// <param name="e">イベント情報です.</param>
        /// <remarks>引数チェックを行っていません. e に null を指定しないでください.</remarks>
        private void OnListModified(EventArgs e)
        {
            if (this.ListModified != null)
            {
                this.ListModified(this, e);
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の要素を列挙します.
        /// </summary>
        /// <typeparam name="T">列挙される要素の型です. DuplicateKeyedSortedList の要素の型と同一です.</typeparam>
        [Serializable]
        private abstract class Enumerator<T> : IEnumerator<T>, IDictionaryEnumerator
        {
            /// <summary>
            /// 列挙対象のリストです.
            /// </summary>
            private DuplicateKeyedSortedList<TKey, TItem> parentList;

            /// <summary>
            /// 次の列挙対象のインデックス位置です.
            /// </summary>
            private int nextIndex;

            /// <summary>
            /// 現在の列挙位置のキーです.
            /// </summary>
            private TKey currentKey;

            /// <summary>
            /// 現在の列挙位置の値です.
            /// </summary>
            private TItem currentValue;

            /// <summary>
            /// 現在の列挙位置の DictionaryEntry です.
            /// </summary>
            private DictionaryEntry currentDictionaryEntry;

            /// <summary>
            /// この Enumerator を初期後, 対象のコレクションに変更が行われたかどうかを示します.
            /// </summary>
            private bool modified;

            /// <summary>
            /// DuplicateKeyedSortedList の要素を列挙するクラスの新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">親の DuplicateKeyedSortedList です.</param>
            /// <exception cref="System.ArgumentNullException">parentList が null です.</exception>
            public Enumerator(DuplicateKeyedSortedList<TKey, TItem> parentList)
            {
                // 引数チェック
                if (parentList == null)
                {
                    throw new ArgumentNullException("parentList");
                }

                // フィールド変数の初期化
                this.parentList = parentList;
                this.nextIndex = 0;
                this.currentKey = default(TKey);
                this.currentValue = default(TItem);
                this.currentDictionaryEntry = default(DictionaryEntry);
                this.modified = false;

                // リスト変更イベントハンドラの設定.
                parentList.ListModified += this.ParentListModified;
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素です.</value>
            public abstract T Current { get; }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>DuplicateKeyedSortedList 内の, 列挙子の現在位置にある要素です.</value>
            /// <exception cref="System.InvalidOperationException">現在位置が最初の要素の前, または最後の要素の後です.</exception>
            object IEnumerator.Current
            {
                get
                {
                    // 状態チェック
                    if (this.nextIndex == 0 || this.nextIndex == -1)
                    {
                        throw new InvalidOperationException(ExceptionMessage_IEnumerator_CurrentOutOfRange);
                    }

                    // 現在位置の要素を返す.
                    return this.Current;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <returns>列挙子の現在位置の要素です.</returns>
            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    // DictionaryEntryを返します.
                    return this.currentDictionaryEntry;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素のキーを取得します.
            /// </summary>
            object IDictionaryEnumerator.Key
            {
                get
                {
                    // キーを返します.
                    return this.currentKey;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素の値を取得します.
            /// </summary>
            object IDictionaryEnumerator.Value
            {
                get
                {
                    // 値を返します.
                    return this.currentValue;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の項目を取得します.
            /// </summary>
            /// <value>列挙子の現在位置のキーです.</value>
            protected TItem CurrentItem
            {
                get
                {
                    return this.currentValue;
                }
            }

            /// <summary>
            /// 列挙子の現在位置のキーを取得します.
            /// </summary>
            /// <value>列挙子の現在位置のキーです.</value>
            protected TKey CurrentKey
            {
                get
                {
                    return this.parentList.GetKeyForItem(this.currentValue);
                }
            }

            /// <summary>
            /// DuplicateKeyedSortedList.Enumerator によって使用されているすべてのリソースを解放します.
            /// </summary>
            public virtual void Dispose()
            {
            }

            /// <summary>
            /// 列挙子を次の要素に進めます.
            /// </summary>
            /// <returns>列挙子が次の要素に正常に進んだ場合はtrue, 列挙子がコレクションの末尾を越えた場合はfalse です.</returns>
            /// <exception cref="System.InvalidOperationException">列挙子が作成された後に, コレクションが変更されました.</exception>
            public virtual bool MoveNext()
            {
                // 状態チェック
                if (this.modified)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0True, "this._modified"));
                }

                // 次の要素に移動できるときは, 移動する.
                // 移動できない場合は, 移動終了している場合は, 何もしない. 今回移動終了の場合は, フィールド変数にその情報を設定
                if (this.nextIndex < this.parentList.size && this.nextIndex != -1)
                {
                    this.currentKey = this.parentList.keys[this.nextIndex];
                    this.currentValue = this.parentList.items[this.nextIndex];
                    this.currentDictionaryEntry = new DictionaryEntry(this.currentKey, this.currentValue);
                    this.nextIndex++;
                    return true;
                }
                else
                {
                    // 既に移動終了している場合は, 何もしない. 今回移動終了の場合は, フィールド変数にその情報を設定
                    if (this.nextIndex == -1)
                    {
                        return false;
                    }
                    else
                    {
                        this.currentKey = default(TKey);
                        this.currentValue = default(TItem);
                        this.currentDictionaryEntry = default(DictionaryEntry);
                        this.nextIndex = -1;
                        return false;
                    }
                }
            }

            /// <summary>
            /// 列挙子を初期位置, つまりコレクションの最初の要素の前に設定します.
            /// </summary>
            /// <exception cref="System.InvalidOperationException">列挙子が作成された後に, コレクションが変更されました.</exception>
            public virtual void Reset()
            {
                // 状態チェック
                if (this.modified)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0True, "this._modified"));
                }

                // 初期設定.
                this.nextIndex = 0;
                this.currentKey = default(TKey);
                this.currentValue = default(TItem);
                this.currentDictionaryEntry = default(DictionaryEntry);
            }

            /// <summary>
            /// 対象リストが変更されたときに発生するイベントのハンドラです.
            /// </summary>
            /// <param name="sender">変更が行われた対象リストです.</param>
            /// <param name="e">イベント情報です.</param>
            /// <remarks>このメソッドはイベントハンドラとしてのみ使用してください. 直接呼び出さないでください.</remarks>
            private void ParentListModified(object sender, EventArgs e)
            {
                this.modified = true;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の要素の KeyValuePair による列挙体です.
        /// </summary>
        [Serializable]
        private sealed class KeyValuePairEnumerator
            : DuplicateKeyedSortedList<TKey, TItem>.Enumerator<KeyValuePair<TKey, TItem>>, IDictionaryEnumerator
        {
            /// <summary>
            /// 列挙子の現在位置の KeyValuePair です.
            /// </summary>
            private KeyValuePair<TKey, TItem> currentKeyValuePair;

            /// <summary>
            /// 列挙子の現在位置の DictionaryEntry です.
            /// </summary>
            private DictionaryEntry currentDictionaryEntry;

            /// <summary>
            /// DuplicateKeyedSortedList 要素 KeyValuePair 列挙体の新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">列挙対象の DuplicateKeyedSortedList です.</param>
            /// <exception cref="System.ArgumentNullException">parentList が null です.</exception>
            public KeyValuePairEnumerator(DuplicateKeyedSortedList<TKey, TItem> parentList)
                : base(parentList)
            {
                this.currentKeyValuePair = default(KeyValuePair<TKey, TItem>);
                this.currentDictionaryEntry = default(DictionaryEntry);
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素です.</value>
            public override KeyValuePair<TKey, TItem> Current
            {
                get
                {
                    // KeyValuePair を返す.
                    return this.currentKeyValuePair;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素です.</value>
            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    // DictionaryEntry を返します.
                    return this.currentDictionaryEntry;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素のキーを取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素のキーです.</value>
            object IDictionaryEnumerator.Key
            {
                get
                {
                    // キーを返します.
                    return this.CurrentKey;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素の値を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素の値です.</value>
            object IDictionaryEnumerator.Value
            {
                get
                {
                    // 値を返します.
                    return this.CurrentItem;
                }
            }

            /// <summary>
            /// 列挙子を次の要素に進めます.
            /// </summary>
            /// <returns>列挙子が次の要素に正常に進んだ場合はtrue, 列挙子がコレクションの末尾を越えた場合はfalse です.</returns>
            /// <exception cref="System.InvalidOperationException">列挙子が作成された後に, コレクションが変更されました.</exception>
            public override bool MoveNext()
            {
                bool moveNextResult = base.MoveNext();

                // 現在位置の KeyValuePair, DictionaryEntry を設定
                if (moveNextResult)
                {
                    this.currentKeyValuePair = new KeyValuePair<TKey, TItem>(this.CurrentKey, this.CurrentItem);
                    this.currentDictionaryEntry = new DictionaryEntry(this.CurrentKey, this.CurrentItem);
                }
                else
                {
                    this.currentKeyValuePair = default(KeyValuePair<TKey, TItem>);
                    this.currentDictionaryEntry = default(DictionaryEntry);
                }

                return moveNextResult;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の要素の DictionaryEntry による列挙体です.
        /// </summary>
        [Serializable]
        private sealed class DictionaryEntryEnumerator
            : DuplicateKeyedSortedList<TKey, TItem>.Enumerator<DictionaryEntry>,
            IDictionaryEnumerator
        {
            /// <summary>
            /// 現在位置の DictionaryEntry です.
            /// </summary>
            private DictionaryEntry currentDictionaryEntry;

            /// <summary>
            /// DuplicateKeyedSortedList 要素 DictionaryEntry 列挙体の新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">列挙対象の DuplicateKeyedSortedList です.</param>
            /// <exception cref="System.ArgumentNullException">parentList が null です.</exception>
            public DictionaryEntryEnumerator(DuplicateKeyedSortedList<TKey, TItem> parentList)
                : base(parentList)
            {
                this.currentDictionaryEntry = default(DictionaryEntry);
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素です.</value>
            public override DictionaryEntry Current
            {
                get
                {
                    // DictionaryEntry を返します.
                    return this.currentDictionaryEntry;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素です.</value>
            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    // DictionaryEntry を返します.
                    return this.currentDictionaryEntry;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素のキーを取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素のキーです.</value>
            object IDictionaryEnumerator.Key
            {
                get
                {
                    // キーを返します.
                    return this.CurrentKey;
                }
            }

            /// <summary>
            /// 列挙子の現在位置の要素の値を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素の値です.</value>
            object IDictionaryEnumerator.Value
            {
                get
                {
                    // 値を返します.
                    return this.CurrentItem;
                }
            }

            /// <summary>
            /// 列挙子を次の要素に進めます.
            /// </summary>
            /// <returns>列挙子が次の要素に正常に進んだ場合はtrue, 列挙子がコレクションの末尾を越えた場合はfalse です.</returns>
            /// <exception cref="System.InvalidOperationException">列挙子が作成された後に, コレクションが変更されました.</exception>
            public override bool MoveNext()
            {
                bool moveNextResult = base.MoveNext();

                if (moveNextResult)
                {
                    this.currentDictionaryEntry = new DictionaryEntry(this.CurrentKey, this.CurrentItem);
                }
                else
                {
                    this.currentDictionaryEntry = default(DictionaryEntry);
                }

                return moveNextResult;
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList のキーの列挙体です.
        /// </summary>
        [Serializable]
        private sealed class KeyEnumerator : DuplicateKeyedSortedList<TKey, TItem>.Enumerator<TKey>, IEnumerator<TKey>
        {
            /// <summary>
            /// DuplicateKeyedSortedList キー列挙体の新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">列挙対象の DuplicateKeyedSortedList です.</param>
            /// <exception cref="System.ArgumentNullException">parentList が null です.</exception>
            public KeyEnumerator(DuplicateKeyedSortedList<TKey, TItem> parentList)
                : base(parentList)
            {
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素です.</value>
            public override TKey Current
            {
                get
                {
                    // 現在のキーを返します.
                    return this.CurrentKey;
                }
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の値の列挙体です.
        /// </summary>
        [Serializable]
        private sealed class ValueEnumerator : DuplicateKeyedSortedList<TKey, TItem>.Enumerator<TItem>, IEnumerator<TItem>
        {
            /// <summary>
            /// DuplicateKeyedSortedList 値列挙体の新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">列挙対象の DuplicateKeyedSortedList です.</param>
            /// <exception cref="System.ArgumentNullException">parentList が null です.</exception>
            public ValueEnumerator(DuplicateKeyedSortedList<TKey, TItem> parentList)
                : base(parentList)
            {
            }

            /// <summary>
            /// 列挙子の現在位置の要素を取得します.
            /// </summary>
            /// <value>列挙子の現在位置の要素です.</value>
            public override TItem Current
            {
                get
                {
                    // 現在のキーを返します.
                    return this.CurrentItem;
                }
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList のコレクションです.
        /// </summary>
        /// <typeparam name="T">コレクションに格納される項目の型です.</typeparam>
        [Serializable, DebuggerDisplay("Count = {Count}")]
        private abstract class Collection<T> : ICollection<T>, ICollection
        {
            /// <summary>
            /// コレクションの要素の取得, 設定対象の DuplicateKeyedSortedList です.
            /// </summary>
            private DuplicateKeyedSortedList<TKey, TItem> parentList;

            /// <summary>
            /// DuplicateKeyedSortedList のコレクションの新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">親の DuplicateKeyedSortedList です.</param>
            /// <remarks>引数の null 検査は行いません.</remarks>
            protected Collection(DuplicateKeyedSortedList<TKey, TItem> parentList)
            {
                this.parentList = parentList;
            }

            /// <summary>
            /// Collection に格納されている要素の数を返します.
            /// </summary>
            /// <value>Collection に格納されている要素の数です.</value>
            public virtual int Count
            {
                get
                {
                    return this.parentList.Count;
                }
            }

            /// <summary>
            /// Collection が読み取り専用であるかを取得します.
            /// </summary>
            /// <value>
            /// <para>Collection が読み取り専用である場合, true. そうでない場合, false です.</para>
            /// </value>
            /// <remarks>Collection は読み取り専用であるため, この値は常に true です.</remarks>
            public virtual bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            /// <summary>
            /// Collection へのアクセスを複数スレッド間で同期するために使用するオブジェクトを取得します.
            /// </summary>
            /// <value>Collection へのアクセスを複数スレッド間で同期するために使用するオブジェクトです.</value>
            public object SyncRoot
            {
                get
                {
                    return this.parentList.SyncRoot;
                }
            }

            /// <summary>
            /// Collection へのアクセスが複数スレッド間で同期されている (スレッドセーフである) かを取得します.
            /// </summary>
            /// <value>
            /// <para>Collection へのアクセスが複数スレッド間で同期されている（スレッドセーフである）場合は true. そうでない場合は false です.</para>
            /// </value>
            /// <remarks>Collection では, アクセス同期機能はサポートされないため, この値は常に false です.</remarks>
            public bool IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            /// <summary>
            /// コレクションの要素の取得, 設定対象の DuplicateKeyedSortedList を取得します.
            /// </summary>
            /// <value>
            /// コレクションの要素の取得, 設定対象の DuplicateKeyedSortedList です.
            /// </value>
            protected DuplicateKeyedSortedList<TKey, TItem> ParentList
            {
                get
                {
                    return this.parentList;
                }
            }

            /// <summary>
            /// 指定されたインデックス位置にある要素を取得します.
            /// </summary>
            /// <param name="index">インデックス位置です.</param>
            /// <returns>指定されたインデックス位置にある要素です.</returns>
            /// <remarks>必ず index が範囲内にあることを保証して呼び出してください.</remarks>
            protected abstract T this[int index]
            {
                get;
            }

            /// <summary>
            /// <para>Collection の末尾に項目を追加します.</para>
            /// </summary>
            /// <param name="item">追加する項目です.</param>
            /// <exception cref="System.NotSupportedException">メソッドの実行はサポートされません.</exception>
            public virtual void Add(T item)
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_NotSupportedBecauseOf0, String_ReadOnly));
            }

            /// <summary>
            /// <para>Collection からすべての要素を削除します.</para>
            /// <para>Collection は読み取り専用であるため, このメソッドは使用できません.</para>
            /// </summary>
            /// <exception cref="System.NotSupportedException">メソッドの実行はサポートされません.</exception>
            public virtual void Clear()
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_NotSupportedBecauseOf0, String_ReadOnly));
            }

            /// <summary>
            /// <para>Collection に指定された要素が格納されているかを返します.</para>
            /// </summary>
            /// <param name="item">格納されているか調べる要素です.</param>
            /// <returns>Collection に指定された要素が格納されている場合, true, そうでない場合, false です..</returns>
            public abstract bool Contains(T item);

            /// <summary>
            /// KeyCollection 内の指定された範囲を配列にコピーします.
            /// </summary>
            /// <param name="array">コピー先の配列です.</param>
            /// <param name="arrayIndex">コピー先の範囲の開始インデックスです.</param>
            /// <exception cref="System.ArgumentException">
            /// <para>arrayIndex が array の長さを超えています. または, コピー元の要素数が, コピー先の格納できる数を超えています.</para>
            /// </exception>
            /// <exception cref="System.ArgumentNullException">array が null です.</exception>
            /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex が 0 未満です.</exception>
            public virtual void CopyTo(T[] array, int arrayIndex)
            {
                // 引数チェック
                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }

                if (arrayIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("arrayIndex", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "arrayIndex", "0"));
                }

                if (arrayIndex > array.Length)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0GreaterThan1, "arrayIndex", "array.Length"), "array");
                }

                if (array.Length - arrayIndex < this.parentList.Count)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1LessThan3, "array", "arrayIndex", String_ItemCount, "this._parentList.Count"));
                }

                // 要素ーを配列にコピー
                int count = this.parentList.Count;
                for (int i = 0; i < count; i++)
                {
                    array[arrayIndex + i] = this[i];
                }
            }

            /// <summary>
            /// ICollection 内の指定された範囲を配列にコピーします.
            /// </summary>
            /// <param name="array">コピー先の配列です.</param>
            /// <param name="index">コピー先の範囲の開始インデックスです.</param>
            /// <exception cref="System.ArgumentException">
            /// <para>array が多次元です. index が array の長さを超えています. コピー元の要素数が, コピー先の格納できる数を超えています.</para>
            /// <para>または, コピー元の型が, コピー先の型に自動的にキャストできません.</para>
            /// </exception>
            /// <exception cref="System.ArgumentNullException">array が null です.</exception>
            /// <exception cref="System.ArgumentOutOfRangeException">index が 0 未満です.</exception>
            void ICollection.CopyTo(Array array, int index)
            {
                // 引数チェック
                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }

                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0LessThan1, "index", "0"));
                }

                if (array.Rank > 1)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0Is1, "array", String_MultiDimension), "array");
                }

                if (index > array.Length)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0GreaterThan1, "index", "array.Length"), "array");
                }

                if (array.Length - index < this.parentList.Count)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_2From0And1LessThan3, "array", "index", String_ItemCount, "this._parentList.Count"));
                }

                // 格納先配列要素の型にキャストできるかを調べる.
                // つまり, 格納先配列要素のクラスと同一クラスであるか, そのサブクラスであるなら良い.
                // そうでない場合は例外
                Type typeListItem = typeof(T);
                Type typeArrayItem = array.GetType().GetElementType();
                if (!typeListItem.Equals(typeArrayItem) &&
                    !typeListItem.IsSubclassOf(typeArrayItem))
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_0HasTypeInvalid, "array"), "array");
                }

                // キーを配列にコピー
                int count = this.parentList.Count;
                for (int i = 0; i < count; i++)
                {
                    array.SetValue(this[i], index + i);
                }
            }

            /// <summary>
            /// <para>ICollection から指定された要素を検索し, 最初に見つかったものを削除します.</para>
            /// <para>ValueCollection は読み取り専用であるため, このメソッドは使用できません.</para>
            /// </summary>
            /// <param name="item">削除する要素です.</param>
            /// <returns>指定された要素が見つかり, 削除された場合は true, それ以外の場合は false です.</returns>
            /// <exception cref="System.NotSupportedException">メソッドの実行はサポートされません.</exception>
            bool ICollection<T>.Remove(T item)
            {
                // 読み取り専用であるのでサポートされない.
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, ExceptionMessage_NotSupportedBecauseOf0, String_ReadOnly));
            }

            /// <summary>
            /// コレクションを反復処理する列挙子を返します.
            /// </summary>
            /// <returns>コレクションを反復処理する列挙子です.</returns>
            public abstract IEnumerator<T> GetEnumerator();

            /// <summary>
            /// コレクションを反復処理する列挙子を返します.
            /// </summary>
            /// <returns>コレクションを反復処理する列挙子です.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList のキーのコレクションです.
        /// </summary>
        [Serializable, DebuggerDisplay("Count = {Count}")]
        private sealed class KeyCollection : DuplicateKeyedSortedList<TKey, TItem>.Collection<TKey>
        {
            /// <summary>
            /// DuplicateKeyedSortedList のキーのコレクションの新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">コレクションのキーの取得, 設定対象の DuplicateKeyedSortedList です.</param>
            /// <exception cref="System.ArgumentNullException">parentList が null です.</exception>
            public KeyCollection(DuplicateKeyedSortedList<TKey, TItem> parentList)
                : base(parentList)
            {
            }

            /// <summary>
            /// 指定されたインデックス位置にある要素を取得します.
            /// </summary>
            /// <param name="index">インデックス位置です.</param>
            /// <returns>指定されたインデックス位置にある要素です.</returns>
            /// <remarks>必ず index が範囲内にあることを保証して呼び出してください.</remarks>
            protected override TKey this[int index]
            {
                get
                {
                    return this.ParentList.GetKey(index);
                }
            }

            /// <summary>
            /// <para>KeyCollection に指定されたキーが格納されているかを返します.</para>
            /// <para>KeyCollection は読み取り専用であるため, このメソッドは使用できません.</para>
            /// </summary>
            /// <param name="item">格納されているか調べるキーです.</param>
            /// <returns>KeyCollection に指定された項目が格納されている場合, true, そうでない場合, false です..</returns>
            /// <exception cref="System.NotSupportedException">メソッドの実行はサポートされません.</exception>
            public override bool Contains(TKey item)
            {
                return this.ParentList.ContainsKey(item);
            }

            /// <summary>
            /// コレクションを反復処理する列挙子を返します.
            /// </summary>
            /// <returns>コレクションを反復処理する列挙子です.</returns>
            public override IEnumerator<TKey> GetEnumerator()
            {
                return new DuplicateKeyedSortedList<TKey, TItem>.KeyEnumerator(this.ParentList);
            }
        }

        /// <summary>
        /// DuplicateKeyedSortedList の値のコレクションです.
        /// </summary>
        [Serializable, DebuggerDisplay("Count = {Count}")]
        private sealed class ValueCollection : DuplicateKeyedSortedList<TKey, TItem>.Collection<TItem>
        {
            /// <summary>
            /// DuplicateKeyedSortedList のキーのコレクションの新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="parentList">コレクションのキーの取得, 設定対象の DuplicateKeyedSortedList です.</param>
            /// <exception cref="System.ArgumentNullException">parentList が null です.</exception>
            public ValueCollection(DuplicateKeyedSortedList<TKey, TItem> parentList)
                : base(parentList)
            {
            }

            /// <summary>
            /// 指定されたインデックス位置にある要素を取得します.
            /// </summary>
            /// <param name="index">インデックス位置です.</param>
            /// <returns>指定されたインデックス位置にある要素です.</returns>
            /// <remarks>必ず index が範囲内にあることを保証して呼び出してください.</remarks>
            protected override TItem this[int index]
            {
                get
                {
                    return this.ParentList.GetByIndex(index);
                }
            }

            /// <summary>
            /// <para>ValueCollection に指定されたキーが格納されているかを返します.</para>
            /// <para>ValueCollection は読み取り専用であるため, このメソッドは使用できません.</para>
            /// </summary>
            /// <param name="item">格納されているか調べるキーです.</param>
            /// <returns>ValueCollection に指定された項目が格納されている場合, true, そうでない場合, false です..</returns>
            /// <exception cref="System.NotSupportedException">メソッドの実行はサポートされません.</exception>
            public override bool Contains(TItem item)
            {
                return this.ParentList.Contains(item);
            }

            /// <summary>
            /// コレクションを反復処理する列挙子を返します.
            /// </summary>
            /// <returns>コレクションを反復処理する列挙子です.</returns>
            public override IEnumerator<TItem> GetEnumerator()
            {
                return new DuplicateKeyedSortedList<TKey, TItem>.ValueEnumerator(this.ParentList);
            }
        }

        /// <summary>
        /// リストのインデックスの範囲を表します.
        /// </summary>
        [Serializable]
        public struct ListIndexRange
        {
            /// <summary>
            /// インデックス範囲の開始インデックスです.
            /// </summary>
            private int index;

            /// <summary>
            /// インデックス範囲の要素数です.
            /// </summary>
            private int count;

            /// <summary>
            /// リストのインデックスの範囲を表す構造体の新しいインスタンスを初期化します.
            /// </summary>
            /// <param name="index">インデックス範囲の開始インデックス位置です.</param>
            /// <param name="count">インデックス範囲の要素数です.</param>
            /// <remarks>index, count の値範囲のチェックは行いません. 負の値を設定することも可能です.</remarks>
            public ListIndexRange(int index, int count)
            {
                // フィールドに値を設定
                this.index = index;
                this.count = count;
            }

            /// <summary>
            /// インデックス範囲の開始インデックスです.
            /// </summary>
            public int Index
            {
                get
                {
                    return this.index;
                }

                set
                {
                    this.index = value;
                }
            }

            /// <summary>
            /// インデックス範囲の要素数です.
            /// </summary>
            public int Count
            {
                get
                {
                    return this.count;
                }

                set
                {
                    this.count = value;
                }
            }

            /// <summary>
            /// 指定された 2 個の ListIndexRange が等価であるかを取得します.
            /// </summary>
            /// <param name="x">1 個目の ListIndexRange です.</param>
            /// <param name="y">2 個目の ListIndexRange です.</param>
            /// <returns>指定された 2 個の ListIndexRange が等価である場合 true, そうでない場合 false です.</returns>
            public static bool operator ==(ListIndexRange x, ListIndexRange y)
            {
                // nullが指定された場合は, 等しくないと判定.
                if ((object)x == null || (object)y == null)
                {
                    if ((object)x == null && (object)y == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                // フィールドの等価によって判定.
                return x.index == y.index && x.count == y.count;
            }

            /// <summary>
            /// 指定された 2 個の ListIndexRange が等価でないかを取得します.
            /// </summary>
            /// <param name="x">1 個目の ListIndexRange です.</param>
            /// <param name="y">2 個目の ListIndexRange です.</param>
            /// <returns>指定された 2 個の ListIndexRange が等価でない場合 true, そうでない場合 false です.</returns>
            public static bool operator !=(ListIndexRange x, ListIndexRange y)
            {
                // nullが指定された場合は, 等しくないと判定.
                if ((object)x == null || (object)y == null)
                {
                    if ((object)x == null && (object)y == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                // フィールドの等価によって判定.
                return !(x.index == y.index && x.count == y.count);
            }

            /// <summary>
            /// ValueRange が指定されたオブジェクトと等価であるかを調べます.
            /// </summary>
            /// <param name="obj">等価であるか調べる対象のオブジェクトです. </param>
            /// <returns>ValueRange が指定されたオブジェクトと等価である場合, true, そうでない場合, false です. </returns>
            public override bool Equals(object obj)
            {
                // nullが指定された場合は, 等しくないと判定.
                if (obj == null)
                {
                    return false;
                }

                // 型が異なる場合は, 等しくないと判定.
                ListIndexRange concreteObj;
                try
                {
                    concreteObj = (ListIndexRange)obj;
                }
                catch (InvalidCastException)
                {
                    return false;
                }

                // フィールドの等価によって判定.
                return this.index == concreteObj.index && this.count == concreteObj.count;
            }

            /// <summary>
            /// ValueRange のハッシュコードを取得します.
            /// </summary>
            /// <returns>ValueRange のハッシュコードです.</returns>
            public override int GetHashCode()
            {
                return this.index.GetHashCode() ^ this.count.GetHashCode();
            }
        }
    }
}