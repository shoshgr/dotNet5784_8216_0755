using System.Collections.Generic;
using System;
using System.Collections;

namespace PL;


    internal class LevelCollection : IEnumerable
    {
        static readonly IEnumerable<BO.Level> s_enums =
        (Enum.GetValues(typeof(BO.Level)) as IEnumerable<BO.Level>)!;
        public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
    }

