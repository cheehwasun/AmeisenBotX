﻿using AmeisenBotX.Core.Dungeon.Objects;
using AmeisenBotX.Core.Jobs.Profiles;
using AmeisenBotX.Core.Movement.Pathfinding.Objects;
using System.Collections.Generic;

namespace AmeisenBotX.Core.Dungeon
{
    public interface IDungeonEngine
    {
        List<DungeonNode> Nodes { get; }

        IDungeonProfile Profile { get; }

        Vector3 DeathEntrancePosition { get; }

        void Enter();

        void Execute();

        void Exit();

        void OnDeath();
    }
}