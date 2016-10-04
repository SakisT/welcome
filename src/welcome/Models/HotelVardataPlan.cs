using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace welcome.Models
{
    public class HotelVardataPlan
    {
        [ForeignKey("Hotel")]
        public Guid HotelVardataPlanID { get; set; }
        public virtual Hotel Hotel { get; set; }

        #region Constants
        private const string DEFAULT_HISTORY_COLOR = "#66b3ff";
        private const string DEFAULT_INHOUSE_COLOR = "#ffb84d";
        private const string DEFAULT_RESERVATION_COLOR = "#85e085";
        private const string DEFAULT_OPTIONRESERVATION_COLOR = "#cccc00";
        private const string DEFAULT_PENDINGDEPOSIT_COLOR = "#ffb3b3";
        #endregion

        #region HistoryColor
        private string _HistoryColor = DEFAULT_HISTORY_COLOR;
        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_HISTORY_COLOR)]
        public string HistoryColor { get { return _HistoryColor; } set { _HistoryColor = value; } }
        #endregion

        #region InHouseColor
        private string _InHouseColor = DEFAULT_INHOUSE_COLOR;
        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_INHOUSE_COLOR)]
        public string InHouseColor { get { return _InHouseColor; } set { _InHouseColor = value; } }
        #endregion

        #region ReservationColor
        private string _ReservationColor = DEFAULT_RESERVATION_COLOR;
        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_RESERVATION_COLOR)]
        public string ReservationColor { get { return _ReservationColor; } set { _ReservationColor = value; } }
        #endregion

        #region PendingDepositColor
        private string _PendingDepositColor = DEFAULT_PENDINGDEPOSIT_COLOR;
        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_PENDINGDEPOSIT_COLOR)]
        public string PendingDepositColor { get { return _PendingDepositColor; } set { _PendingDepositColor = value; } }
        #endregion

        #region OptionReservationColor
        private string _OptionReservationColor = DEFAULT_OPTIONRESERVATION_COLOR;
        [StringLength(12)]
        [System.ComponentModel.DefaultValue(DEFAULT_OPTIONRESERVATION_COLOR)]
        public string OptionReservationColor { get { return _OptionReservationColor; } set { _OptionReservationColor = value; } }
        #endregion

        private LabelAddin _LabelAddins = LabelAddin.None;
        public LabelAddin LabelAddins { get { return _LabelAddins; } set { _LabelAddins = value; } }
        public enum LabelAddin
        {
            None = 2,
            Agent = 4,
            Board = 8,
            Pax = 16
        }
        public bool DisplayAgentCode { get; set; }


    }
}
