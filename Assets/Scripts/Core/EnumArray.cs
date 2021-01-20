using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage
{

    [Serializable]
    public class EnumArray<TEnum, TMapped> where TEnum : Enum
    {

        [SerializeField] [HideInInspector] [UsedImplicitly] private string enumTypeName;
        [SerializeField] private MappedEnum[] enums;


        public TMapped this[TEnum @enum] => enums.First(e => e.enumValue.Equals(@enum)).mappedValue;


        public EnumArray()
        {
            enums = MapEnum().ToArray();
            enumTypeName = typeof(TEnum).FullName;
        }

        private IEnumerable<MappedEnum> MapEnum() =>
            Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new MappedEnum(e, default));


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