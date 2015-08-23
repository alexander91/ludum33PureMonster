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

        const int MaxNeighborsForCreation = 3;

        void CreateNewPart()
        {
            if (Game.Instance.Manager.NumberOfParts > 100) return;

            if (monsterPart.neighbors.Count < MaxNeighborsForCreation)
            {
                var newObj = (GameObject)Instantiate(monsterPart.myCopy);
                newObj.transform.position = monsterPart.pos + RandomUtils.getRandomVector(1.0f);
                MonsterPart newPart = newObj.GetComponent<MonsterPart>();
                if (newPart == null)
                {
                    Debug.Log("Bad monster object instantiation!!!");
                }
                monsterPart.addNeighbor(newPart);
                if (monsterPart.neighbors.Count == MaxNeighborsForCreation)
                {
                    //monsterPart.neighbors[0].addNeighbor(monsterPart.neighbors[MaxNeighborsForCreation-1]);
                }
            }
            else
            {
                monsterPart.neighbors[UnityEngine.Random.Range(0, monsterPart.neighbors.Count - 1)].copyStrategy.CreateNewPart();
            }
        }

        public void IterateForCreation()
        {
            counter++;

            if (counter > 0)
            {
                CreateNewPart();
                CreateNewPart();
                counter = 0;
            }
        }
    }
}
