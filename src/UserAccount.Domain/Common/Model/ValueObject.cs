using System;
using System.Collections.Generic;
using System.Text;

namespace UserAccount.Domain.Common.Model
{

    public abstract class ValueObject
    {
        /// <summary>
        /// Tests equality
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if the objects are equals, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var valueObject = obj as ValueObject;

            if (ReferenceEquals(valueObject, null))
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return EqualsCore(valueObject);
        }

        /// <summary>
        /// Tests equality
        /// </summary>
        /// <param name="other">The object to compare</param>
        /// <returns>True if the objects are equals, false otherwise</returns>
        protected abstract bool EqualsCore(ValueObject other);

        /// <summary>
        /// Get hashcode
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        /// <summary>
        /// Get hashcode
        /// </summary>
        /// <returns>The hashcode</returns>
        protected abstract int GetHashCodeCore();

        /// <summary>
        /// Tests equality between two value objects
        /// </summary>
        /// <param name="a">The first value object</param>
        /// <param name="b">The second value object</param>
        /// <returns>True if the value objects are equal, false otherwise</returns>
        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// Tests inequality between two value objects
        /// </summary>
        /// <param name="a">The first value object</param>
        /// <param name="b">The second value object</param>
        /// <returns>True if the value objects are different, false otherwise</returns>
        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
