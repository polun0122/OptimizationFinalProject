using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    internal class Person
    {
        double peeingTime = 21; /* 如廁時間 */
        double timeCostWeight = 1; /* 時間成本權重 */
        double privateCostWeight = 1; /* 隱私成本權重 */
        double sideOccurpiedWeight = 1; /* 左右兩側被占用的成本權重(隱私成本內) */
        double backOccurpiedWeight = 0.5; /* 後方被占用的成本權重(隱私成本內) */

        double walkingSpeed = 1.48; /* 成人步行速度 */

        public Person()
        {

        }

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
        /// 步行速度，單位: m/s
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
    }
}
