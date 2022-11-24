using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    internal class Toilet
    {
        float restroomLength = 10; /* 廁所總長，單位：公尺 */
        float restroomWidth = 10; /* 廁所總寬，單位：公尺 */
        double toiletLength = 0.5; /* 小便斗長，單位：公尺 */
        double toiletWidth = 0.5; /* 小便斗寬，單位：公尺 */

        int rowAmount; /* 小便斗排數 */
        int toiletAmountEachRow; /* 每排小便斗個數 */
        
        int totalAmount; /* 小便斗總數 */
        double toiletInterval; /* 小便斗間距 */
        double rowInterval; /* 排間距 */

        bool[,] isToiletOccupied;

        /// <summary>
        /// </summary>
        /// <param name="rowAmount">小便斗排數</param>
        /// <param name="toiletAmountEachRow">每排小便斗個數</param>
        /// <exception cref="Exception"></exception>
        public Toilet(int rowAmount, int toiletAmountEachRow)
        {
            if (rowAmount < 2 || toiletAmountEachRow < 0)
            {
                throw new Exception();
            }
            this.rowAmount = rowAmount;
            this.toiletAmountEachRow = toiletAmountEachRow;
            totalAmount = 2 * (rowAmount - 1) * toiletAmountEachRow;
            toiletInterval = (restroomLength - toiletLength * toiletAmountEachRow) / (toiletAmountEachRow + 1);
            rowInterval = restroomWidth / (rowAmount - 1);
            isToiletOccupied = new bool[rowAmount, toiletAmountEachRow];
        }

        /// <summary>
        /// 小便斗總數
        /// </summary>
        /// <returns></returns>
        public int ToiletAmount() { return totalAmount; }

        /// <summary>
        /// 小便斗間距(不含小便斗長度)
        /// </summary>
        /// <returns></returns>
        public double ToiletInterval() { return toiletInterval; }

        /// <summary>
        /// 每一列牆壁間距(包含小便斗寬度)
        /// </summary>
        /// <returns></returns>
        public double RowInterval() { return rowInterval; }

        /// <summary>
        /// 占用特定編號的小便斗 Occupy the specific toilet.
        /// </summary>
        /// <param name="toiletNumber">The number of the toilet. Min: 1, Max: ToiletAmount</param>
        /// <returns>Successfully change or not</returns>
        /// <exception cref="Exception">小便斗編號不合法或該小便斗為占用狀態</exception>
        public bool OccupyToilet(int toiletNumber)
        {
            int row;
            int column;
            if (0 < toiletNumber && toiletNumber <= totalAmount)
            {
                row = (toiletNumber - 1) / rowAmount;
                column = (toiletNumber - 1) % rowAmount;
                if (isToiletOccupied[row, column] == false)
                {
                    isToiletOccupied[row, column] = true;
                    return true;
                }
            }
            throw new Exception();
        }

        /// <summary>
        /// 釋放特定編號的小便斗 Release the specific toilet.
        /// </summary>
        /// <param name="toiletNumber">The number of the toilet. Min: 1, Max: ToiletAmount</param>
        /// <returns>Successfully change or not</returns>
        /// <exception cref="Exception">小便斗編號不合法或該小便斗非占用狀態</exception>
        public bool ReleaseToilet(int toiletNumber)
        {
            int row;
            int column;
            if (0 < toiletNumber && toiletNumber <= totalAmount)
            {
                row = (toiletNumber - 1) / rowAmount;
                column = (toiletNumber - 1) % rowAmount;
                if (isToiletOccupied[row, column] == true)
                {
                    isToiletOccupied[row, column] = false;
                    return true;
                }
            }
            throw new Exception();
        }

        /// <summary>
        /// Return is the specific toilet being occupied.
        /// </summary>
        /// <param name="toiletNumber">The number of the toilet. Min: 1, Max: ToiletAmount</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool IsToiletOccupied(int toiletNumber)
        {
            int row;
            int column;
            if (toiletNumber <= totalAmount)
            {
                row = (toiletNumber - 1) / rowAmount;
                column = (toiletNumber - 1) % rowAmount;
                return isToiletOccupied[row, column];
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Return the distance to the specific toilet. Unit: meter
        /// </summary>
        /// <param name="toiletNumber">The number of the toilet. Min: 1, Max: ToiletAmount</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public double DistanceToToilet(int toiletNumber)
        {
            int aisleNumber;
            int numberInRow;
            if (0 < toiletNumber && toiletNumber <= totalAmount)
            {
                aisleNumber = (toiletNumber - 1) / rowAmount / 2;
                numberInRow = (toiletNumber - 1) % rowAmount + 1;

                double disMoveToRow;
                double disMoveToToilet;
                double disMoveForward;
                disMoveToRow = Math.Abs(restroomWidth / 2/*Start Pos*/ - (aisleNumber * rowInterval + 0.5 * rowInterval/*Target Pos*/));
                disMoveToToilet = (toiletInterval + toiletLength) * numberInRow - 0.5 * toiletLength;
                disMoveForward = 0.5 * rowInterval - toiletWidth;
                return disMoveToRow + disMoveToToilet + disMoveForward;
            }
            throw new Exception();
        }

        /// <summary>
        /// 以文字繪製小便斗排列示意圖
        /// </summary>
        /// <returns></returns>
        public string Distribution()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            for (int i = 1; i <= rowAmount; i++)
            {
                if (i == 1) /* 第一排 */
                {
                    sb.Insert(sb.Length, "-*", toiletAmountEachRow);
                    sb.Append("-\n");
                    
                    sb.Append("|");
                    sb.Insert(sb.Length, " ", toiletAmountEachRow * 2 - 1);
                    sb.Append("|\n");
                }
                else if (i== rowAmount) /* 最後一排 */
                {
                    sb.Insert(sb.Length, "-*", toiletAmountEachRow);
                    sb.Append("-\n");
                }
                else /* 中間排 */
                {
                    sb.Insert(sb.Length, "-*", toiletAmountEachRow);
                    sb.Append("-\n");
                    sb.Insert(sb.Length, "-*", toiletAmountEachRow);
                    sb.Append("-\n|");
                    sb.Insert(sb.Length, " ", toiletAmountEachRow * 2 - 1);
                    sb.Append("|\n");
                }
            }
            sb.AppendLine();
            sb.AppendLine(String.Format(" - Toilet Interval {0:.000} m", toiletInterval));
            sb.AppendLine(String.Format(" | Row    Interval {0:.000} m", rowInterval));
            sb.Append("\n");
            return sb.ToString();
        }
    }
}
