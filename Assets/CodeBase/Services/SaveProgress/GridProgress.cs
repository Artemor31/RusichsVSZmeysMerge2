﻿using System;
using Databases.Data;

namespace Services.SaveProgress
{
    [Serializable]
    public class GridProgress
    {
        public ActorData[,] UnitIds;
        public GridProgress(ActorData[,] unitId) => UnitIds = unitId;
        public GridProgress() => UnitIds = null;
    }
}