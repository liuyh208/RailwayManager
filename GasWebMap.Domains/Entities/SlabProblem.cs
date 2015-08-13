using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Core.Data;
using ServiceStack.ServiceHost;

namespace GasWebMap.Domain
{
    /// <summary>
    /// 轨道板问题
    /// </summary>
    public class SlabProblem : EntityBase
    {
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        /// <value>The workshop.</value>
        public string Workshop { get; set; }
        /// <summary>
        /// 线别
        /// </summary>
        /// <value>The workshop.</value>
        public string RailWay { get; set; }

        /// <summary>
        /// 行别
        /// </summary>
        /// <value>The type of the run.</value>
        public string RunType { get; set; }

        /// <summary>
        /// 里程
        /// </summary>
        /// <value>The mileage.</value>
        public string Mileage { get; set; }

        /// <summary>
        /// 起点板号
        /// </summary>
        /// <value>The start board identifier.</value>
        public string StartBoardID { get; set; }

        /// <summary>
        /// 承轨台号
        /// </summary>
        /// <value>The support rail identifier.</value>
        public string SupportRailID { get; set; }

        /// <summary>
        /// 伤损项目
        /// </summary>
        /// <value>The hurt item.</value>
        public string HurtItem { get; set; }

        /// <summary>
        /// 伤损位置
        /// </summary>
        /// <value>The device accuracy.</value>
        public string HurtPosition { get; set; }

        /// <summary>
        /// 伤损形式
        /// </summary>
        /// <value>The factory.</value>
        public string HurtStyle { get; set; }

        /// <summary>
        ///长
        /// </summary>
        /// <value>The cycle.</value>
        public double Length { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        /// <value>The cycle.</value>
        public double Width { get; set; }


        /// <summary>
        ///高
        /// </summary>
        /// <value>The cycle.</value>
        public double Height { get; set; }


        /// <summary>
        ///伤损等级
        /// </summary>
        /// <value>The cycle.</value>
        public string HurtLevel { get; set; }

        /// <summary>
        /// 检测日期
        /// </summary>
        /// <value>The check date.</value>
        public string CheckDate { get; set; }

        /// <summary>
        /// 检测人
        /// </summary>
        /// <value>The identify date.</value>
        public string Checker { get; set; }

        /// <summary>
        /// 影像资料
        /// </summary>
        /// <value>The valid date.</value>
        public string Movie { get; set; }

        /// <summary>
        /// 采取措施
        /// </summary>
        /// <value>The identify factory.</value>
        public string TakeMeasures { get; set; }

        /// <summary>
        /// 销号日期
        /// </summary>
        /// <value>The log out date.</value>
        public string LogoutDate { get; set; }

        /// <summary>
        /// 销号人
        /// </summary>
        /// <value>The identify certificate no.</value>
        public string LogoutMan { get; set; }

        public string Remark { get; set; }
    }

}
