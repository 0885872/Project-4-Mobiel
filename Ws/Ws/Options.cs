using System;

namespace Options
{
    public interface Option<T>
    {
        U visit<U>(Func<T, U> onSome, Func<U> onNone);
        bool IsSome();
        bool IsNone();
    }

    public class Some<T> : Option<T>
    {
        T value;

        public Some(T val)
        {
            value = val;
        }

        public bool IsSome()
        {
            return true;
        }

        public bool IsNone()
        {
            return false;
        }

        public U visit<U>(Func<T, U> onSome, Func<U> onNone)
        {
            return onSome(value);
        }
    }

    public class None<T> : Option<T>
    {
        public None()
        {

        }

        public bool IsSome()
        {
            return false;
        }

        public bool IsNone()
        {
            return true;
        }

        public U visit<U>(Func<T, U> onSome, Func<U> onNone)
        {
            return onNone();
        }
    }
}
