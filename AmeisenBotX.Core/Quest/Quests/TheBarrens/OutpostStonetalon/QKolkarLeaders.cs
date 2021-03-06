using AmeisenBotX.Core.Movement.Pathfinding.Objects;
using AmeisenBotX.Core.Quest.Objects.Objectives;
using AmeisenBotX.Core.Quest.Objects.Quests;
using System.Collections.Generic;

namespace AmeisenBotX.Core.Quest.Quests.TheBarrens.OutpostStonetalon
{
    class QKolkarLeaders : BotQuest
    {
        public QKolkarLeaders(WowInterface wowInterface)
            : base(wowInterface, 850, "Kolkar Leaders", 11, 1,
                () => (wowInterface.ObjectManager.GetClosestWowUnitByNpcId(new List<int> { 3389 }), new Vector3(-307.14f, -1971.95f, 96.48f)),
                () => (wowInterface.ObjectManager.GetClosestWowUnitByNpcId(new List<int> { 3389 }), new Vector3(-307.14f, -1971.95f, 96.48f)),
                new List<IQuestObjective>()
                {
                    new QuestObjectiveChain(new List<IQuestObjective>()
                    {
                        new KillAndLootQuestObjective(wowInterface, new List<int> { 3394 }, 1, 5022, new List<List<Vector3>> {
                            new()
                            {
                                new Vector3(23.49f, -1714.62f, 101.47f),
                            },
                        }),
                    })
                })
        {}
    }
}
