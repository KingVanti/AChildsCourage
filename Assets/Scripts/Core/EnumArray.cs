using System;
using System.Linq;
using UnityEngine;

namespace AChildsCourage
{

    [Serializable]
    public class EnumArray<TEnum, TMapped> where TEnum : Enum
    {

        [SerializeField] private MappedEnum[] enums;


        public TMapped this[TEnum @enum] => enums.First(e => e.enumValue.Equals(@enum)).mappedValue;


        public EnumArray() =>
            enums = Enum.GetValues(typeof(TEnum))
                        .Cast<TEnum>()
                        .Select(e => new MappedEnum(e, default))
                        .ToArray();


        [Serializable]
        private struct MappedEnum
        {

            [SerializeField] public TEnum enumValue;
            [SerializeField] public TMapped mappedValue;


            public MappedEnum(TEnum enumValue, TMapped mappedValue)
            {
                this.enumValue = enumValue;
                this.mappedValue = mappedValue;
            }

        }

    }

}