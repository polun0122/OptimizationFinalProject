using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    internal class Person
    {
        public enum Strategy { LazyBehavior, CooperativeBehavior, MaximizeDistanceBehavior };

        double peeingTime = 21; /* 如廁時間 */
        double walkingSpeed = 1.48; /* 成人步行速度 */

        Strategy strategy; /* 小便斗選擇策略 */
        double timeCostWeight; /* 時間成本權重 */
        double privateCostWeight; /* 隱私成本權重 */
        double sideOccurpiedWeight; /* 左右兩側被占用的成本權重(隱私成本內) */
        double backOccurpiedWeight; /* 後方被占用的成本權重(隱私成本內) */


        public Person(Strategy strategy)
        {
            this.strategy = strategy; /* 小便斗選擇策略 */
            timeCostWeight = 1; /* 時間成本權重 */
            privateCostWeight = 1; /* 隱私成本權重 */
            sideOccurpiedWeight = 1; /* 左右兩側被占用的成本權重(隱私成本內) */
            backOccurpiedWeight = 0.5; /* 後方被占用的成本權重(隱私成本內) */
        }

        public bool StartWalking;
        public bool StartPeeing;
        public int[] PreferToiletIndex;

        /// <summary>
        /// 排隊等待時間
        /// </summary>
        public double TimeWaiting;

        /// <summary>
        /// 移動至小便斗時間
        /// </summary>
        public double TimeWalking;

        /// <summary>
        /// 右邊有人的時間
        /// </summary>
        public double TimeRightOccurpied;

        /// <summary>
        /// 左邊有人的時間
        /// </summary>
        public double TimeLeftOccurpied;

        /// <summary>
        /// 後面有人的時間
        /// </summary>
        public double TimeBackOccurpied;

        /// <summary>
        /// 步行速度，單位: 公尺/秒
        /// </summary>
        /// <returns></returns>
        public double WalkingSpeed() { return walkingSpeed; }

        /// <summary>
        /// 上廁所時長
        /// </summary>
        /// <returns></returns>
        public double TimePeeing() { return peeingTime; }

        /// <summary>
        /// 計算個人總成本
        /// </summary>
        /// <returns></returns>
        public double TotalCost()
        {
            double totalCost;
            totalCost = timeCostWeight * GetTimeCost() + privateCostWeight * GetPrivateCost();
            return totalCost;
        }

        /// <summary>
        /// 計算個人隱私成本
        /// </summary>
        /// <returns></returns>
        public double GetPrivateCost()
        {
            double privateCost;
            privateCost = (TimeRightOccurpied + TimeLeftOccurpied) * sideOccurpiedWeight +
                           TimeBackOccurpied * backOccurpiedWeight;
            return privateCost;
        }

        /// <summary>
        /// 計算個人時間成本
        /// </summary>
        /// <returns></returns>
        public double GetTimeCost()
        {
            double timeCost;
            timeCost = TimeWaiting + TimeWalking + TimePeeing();
            return timeCost;
        }


        public int[] ChosenToiletIndex(Toilet toilet)
        {
            Queue availableToilets = new Queue();
            Queue occupiedToilets = new Queue();

            /* 若廁所已經全滿直接回傳 -1,-1 */
            for (int i = 0; i < toilet.GetRowAmount(); i++)
            {
                for (int j = 0; j < toilet.GetToiletAmountPerRow(); j++)
                {
                    if (!toilet.IsToiletOccupied(i, j))
                        availableToilets.Enqueue(new int[] { i, j });
                    else
                        occupiedToilets.Enqueue(new int[] { i, j });
                }
            }
            if (availableToilets.Count == 0)
                return new int[] { -1, -1 };

            if (strategy == Strategy.MaximizeDistanceBehavior)
            {
                /* MaximizeDistanceBehavior */
                Queue eligibleToilets = MaximizeDistanceBehavior(availableToilets, occupiedToilets, toilet);
                return (int[])eligibleToilets.Dequeue();
            }
            else if (strategy == Strategy.CooperativeBehavior)
            {
                /* CooperativeBehavior */

            }
            else if (strategy == Strategy.LazyBehavior)
            {
                /* LazyBehavior */
                Queue eligibleToilets = LazyBehavior(availableToilets, toilet);
                return (int[])eligibleToilets.Dequeue();
            }

            return new int[] { 999, 999 };
        }

        private Queue LazyBehavior(Queue availableToilets, Toilet toilet)
        {
            Queue eligibleList = new Queue();

            double minWalkingTime = double.MaxValue;
            foreach (int[] toiletIdx in availableToilets)
            {
                double timeToToilet = toilet.DistanceToToilet(toiletIdx[0], toiletIdx[1]) / WalkingSpeed();
                if (timeToToilet < minWalkingTime)
                {
                    minWalkingTime = timeToToilet;
                    eligibleList.Clear();
                    eligibleList.Enqueue(toiletIdx);
                }
                else if (timeToToilet == minWalkingTime)
                {
                    eligibleList.Enqueue(toiletIdx);
                }
            }
            return eligibleList;
        }

        private Queue MaximizeDistanceBehavior(Queue availableToilets, Queue occupiedToilets, Toilet toilet)
        {
            Queue eligibleList = new Queue();

            double maxDistance = double.MinValue;
            double rowIntreval = toilet.GetRowInterval();
            double toiletInterval = toilet.GetToiletInterval();

            foreach (int[] availableToiletIdx in availableToilets)
            {
                double minNeighborDistance = double.MaxValue;
                foreach (int[] occupiedToiletIdx in occupiedToilets)
                {
                    double neighborDistance = (Math.Abs(availableToiletIdx[0] - occupiedToiletIdx[0])) * rowIntreval + (Math.Abs(availableToiletIdx[1] - occupiedToiletIdx[1])) * toiletInterval;
                    if (neighborDistance < minNeighborDistance)
                    {
                        minNeighborDistance = neighborDistance;
                    }
                }

                if (minNeighborDistance > maxDistance)
                {
                    maxDistance = minNeighborDistance;
                    eligibleList.Clear();
                    eligibleList.Enqueue(availableToiletIdx);
                }
                else if (minNeighborDistance == maxDistance)
                {
                    eligibleList.Enqueue(availableToiletIdx);
                }
            }
            return eligibleList;
        }

        private Queue CooperativeBehavior(Queue availableToilets, Queue occupiedToilets, Toilet toilet)
        {
            Queue eligibleList = new Queue();



            return eligibleList;
        }
    }
}
