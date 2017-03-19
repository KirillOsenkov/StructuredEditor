//namespace System.Collections.Generic
//{
//// current ICollection
//public interface ICollection<T> : IEnumerable<T>, IEnumerable
//{
//    int Count { get; }
//    bool IsReadOnly { get; }
//    void Add(T item);
//    void Clear();
//    bool Contains(T item);
//    void CopyTo(T[] array, int arrayIndex);
//    bool Remove(T item);
//}

//// new version
//public interface ICollection<T> : IEnumerable<T>, IEnumerable
//, IAllowsAdd<T>, IAllowsRemove<T>, IClearable, ICountable, ISet<T>
//{
//    bool IsReadOnly { get; }
//    void CopyTo(T[] array, int arrayIndex);
//}

//public interface IAllowsAdd<T>
//{
//    void Add(T item);
//}

//public interface IAllowsRemove<T>
//{
//    bool Remove(T item);
//}

//public interface IClearable
//{
//    void Clear();
//}

//public interface ICountable
//{
//    int Count { get; }
//}

//public interface ISet<T>
//{
//    bool Contains(T item);
//}
//}

