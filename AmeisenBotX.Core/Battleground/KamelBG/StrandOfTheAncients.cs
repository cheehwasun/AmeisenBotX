﻿using AmeisenBotX.Core.Data.Objects.WowObjects;
using AmeisenBotX.Core.Movement.Pathfinding.Objects;
using AmeisenBotX.Core.Battleground.KamelBG.Enums;
using System;
using System.Linq;
using AmeisenBotX.Core.Movement.Enums;
using AmeisenBotX.Core.Common;
using System.Collections.Generic;
using AmeisenBotX.Logging;

namespace AmeisenBotX.Core.Battleground.KamelBG
{
    internal class StrandOfTheAncients : IBattlegroundEngine
    {
        public string Author => "Lukas";

        public string Description => "Strand of the Ancients";

        public string Name => "Strand of the Ancients";

        public List<Vector3> PathRight { get; } = new List<Vector3>()
        {
            new Vector3(1403, 69, 30)
        };

        private TimegatedEvent CombatEvent { get; }

        public WowInterface WowInterface { get; }

        public StrandOfTheAncients(WowInterface wowInterface)
        {
            WowInterface = wowInterface;

            CombatEvent = new TimegatedEvent(TimeSpan.FromSeconds(2));
        }

        public void Enter()
        {

        }
        public void Execute()
        {
            Combat();

            if (WowInterface.ObjectManager.Vehicle == null)
            {

                WowGameobject VehicleNode = WowInterface.ObjectManager.WowObjects
                    .OfType<WowGameobject>()
                    .Where(x => Enum.IsDefined(typeof(Vehicle), x.DisplayId)
                            && x.Position.GetDistance(WowInterface.ObjectManager.Player.Position) < 20)
                    .OrderBy(x => x.Position.GetDistance(WowInterface.ObjectManager.Player.Position))
                    .FirstOrDefault();

                if (VehicleNode != null)
                {
                    WowInterface.MovementEngine.SetMovementAction(MovementAction.Move, VehicleNode.Position);

                    if (WowInterface.ObjectManager.Player.Position.GetDistance(VehicleNode.Position) <= 4)
                    {
                        WowInterface.MovementEngine.StopMovement();

                        WowInterface.HookManager.WowObjectRightClick(VehicleNode);

                    }
                }
            }
            else
            {
                Vector3 currentNode = PathRight[0];
                WowInterface.MovementEngine.SetMovementAction(MovementAction.Move, currentNode);
            }
        }


        public void Leave()
        {

        }

        public void Combat()
        {
            WowPlayer weakestPlayer = WowInterface.ObjectManager.GetNearEnemies<WowPlayer>(WowInterface.ObjectManager.Player.Position, 30.0).OrderBy(e => e.Health).FirstOrDefault();

            if (weakestPlayer != null)
            {
                double distance = weakestPlayer.Position.GetDistance(WowInterface.ObjectManager.Player.Position);
                double threshold = WowInterface.CombatClass.IsMelee ? 3.0 : 28.0;

                if (distance > threshold)
                {
                    WowInterface.MovementEngine.SetMovementAction(MovementAction.Move, weakestPlayer.Position);
                }
                else if (CombatEvent.Run())
                {
                    WowInterface.Globals.ForceCombat = true;
                    WowInterface.HookManager.WowTargetGuid(weakestPlayer.Guid);
                }
            }
            else
            {

            }
        }
    }
}
