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
        double toiletLength = 0.3; /* 小便斗長，單位：公尺 */
        double toiletWidth = 0.33; /* 小便斗寬，單位：公尺 */

        int wallAmount; /* 牆壁排數 */
        int rowAmount; /* 小便斗排數 */
        int toiletAmountPerRow; /* 每排小便斗個數 */
        
        int totalAmount; /* 小便斗總數 */
        double toiletInterval; /* 小便斗間距 */
        double rowInterval; /* 排間距 */

        bool[,] toiletStatus;

        /// <summary>
        /// </summary>
        /// <param name="wallAmount">牆壁排數，需大於2</param>
        /// <param name="toiletAmountPerRow">每排小便斗個數，需大於0</param>
        /// <exception cref="Exception"></exception>
        public Toilet(int wallAmount, int toiletAmountPerRow)
        {
            if (wallAmount < 2 || toiletAmountPerRow < 0)
            {
                throw new Exception();
            }
            this.wallAmount = wallAmount;
            this.toiletAmountPerRow = toiletAmountPerRow;
            rowAmount = 2 * (wallAmount - 1);
            totalAmount = rowAmount * this.toiletAmountPerRow;
            toiletInterval = (restroomLength - toiletLength * this.toiletAmountPerRow) / (this.toiletAmountPerRow + 1);
            rowInterval = restroomWidth / (this.wallAmount - 1);
            toiletStatus = new bool[rowAmount, this.toiletAmountPerRow];
        }

        /// <summary>
        /// 小便斗排數
        /// </summary>
        /// <returns></returns>
        public int GetRowAmount() { return rowAmount; }

        /// <summary>
        /// 每排小便斗個數
        /// </summary>
        /// <returns></returns>
        public int GetToiletAmountPerRow() { return toiletAmountPerRow; }

        /// <summary>
        /// 小便斗總數
        /// </summary>
        /// <returns></returns>
        public int GetToiletAmount() { return totalAmount; }

        /// <summary>
        /// 小便斗間距(不含小便斗長度)
        /// </summary>
        /// <returns></returns>
        public double GetToiletInterval() { return toiletInterval; }

        /// <summary>
        /// 每一列牆壁間距(包含小便斗寬度)
        /// </summary>
        /// <returns></returns>
        public double GetRowInterval() { return rowInterval; }

        /// <summary>
        /// 占用特定編號的小便斗 Occupy the specific toilet.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>是否成功更改狀態</returns>
        public bool OccupyToilet(int row, int col)
        {
            if (toiletStatus[row, col] == false)
            {
                toiletStatus[row, col] = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 釋放特定編號的小便斗 Release the specific toilet.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>是否成功更改狀態</returns>
        public bool ReleaseToilet(int row, int col)
        {
            if (toiletStatus[row, col] == true)
            {
                toiletStatus[row, col] = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 回傳該小便斗是否被占用
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool IsToiletOccupied(int row, int col)
        {
            return toiletStatus[row, col];
        }

        /// <summary>
        /// 回傳到該小便斗的距離，單位：公尺。
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public double DistanceToToilet(int row, int col)
        {
            int aisleNumber = row / 2 + 1;
            int numberInRow = col + 1;

            double disMoveToRow;
            double disMoveToToilet;
            double disMoveForward;
            disMoveToRow = Math.Abs(restroomWidth / 2/*Start Pos*/ - (aisleNumber * rowInterval - 0.5 * rowInterval/*Target Pos*/));
            disMoveToToilet = (toiletInterval + toiletLength) * numberInRow - 0.5 * toiletLength;
            disMoveForward = 0.5 * rowInterval - toiletWidth;
            return disMoveToRow + disMoveToToilet + disMoveForward;
        }

        /// <summary>
        /// 以文字繪製小便斗排列示意圖
        /// </summary>
        /// <returns></returns>
        public string Distribution()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            for (int i = 1; i <= wallAmount; i++)
            {
                if (i == 1) /* 第一排 */
                {
                    sb.Insert(sb.Length, "-*", toiletAmountPerRow);
                    sb.Append("-\n");
                    
                    sb.Append("|");
                    sb.Insert(sb.Length, " ", toiletAmountPerRow * 2 - 1);
                    sb.Append("|\n");
                }
                else if (i== wallAmount) /* 最後一排 */
                {
                    sb.Insert(sb.Length, "-*", toiletAmountPerRow);
                    sb.Append("-\n");
                }
                else /* 中間排 */
                {
                    sb.Insert(sb.Length, "-*", toiletAmountPerRow);
                    sb.Append("-\n");
                    sb.Insert(sb.Length, "-*", toiletAmountPerRow);
                    sb.Append("-\n|");
                    sb.Insert(sb.Length, " ", toiletAmountPerRow * 2 - 1);
                    sb.Append("|\n");
                }
            }
            sb.AppendLine();
            sb.AppendLine(String.Format(" - Toilet Interval {0:.000} m", toiletInterval));
            sb.AppendLine(String.Format(" | Row    Interval {0:.000} m", rowInterval));
            sb.Append("\n");
            return sb.ToString();
        }

        public bool[,] ToiletStatus()
        {
            return toiletStatus;
        }
        
       
    }
}
