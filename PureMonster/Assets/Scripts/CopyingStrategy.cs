using UnityEngine;
using System.Collections;

partial class MonsterPart
{
    class CopyingStrategy
    {
        int counter;

        MonsterPart monsterPart;
        public CopyingStrategy(MonsterPart part)
        {
            monsterPart = part;
        }

        void CreateNewPart()
        {
            if (monsterPart.neighbors.Count < 4)
            {
                var newObj = Instantiate<GameObject>(monsterPart.myCopy);
                MonsterPart newPart = newObj.GetComponent<MonsterPart>();
                monsterPart.addNeighbor(newPart);
            }
        }

        public void IterateForCreation()
        {
            counter++;

            if (counter > 20*60)
            {

            }
        }
    }
}
