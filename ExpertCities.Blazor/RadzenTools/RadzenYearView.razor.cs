using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen.Blazor.Rendering;

namespace ExpertCities.Blazor.RadzenTools
{
    public partial class RadzenYearView : SchedulerViewBase
    {
        public override string Icon => "calendar_view_month";

        public override string Title
        {
            get => Scheduler.CurrentDate.ToString("yyyy", Scheduler.Culture);
        }
        /// <inheritdoc />
        [Parameter]
        public override string Text { get; set; } = "Year";
        /// <summary>
        /// Gets or sets the time format.
        /// </summary>
        /// <value>The time format. Set to <c>h tt</c> by default.</value>
        [Parameter]
        public string TimeFormat { get; set; } = "h tt";
        /// <summary>
        /// Gets or sets the format used to display the header text.
        /// </summary>
        /// <value>The header text format. Set to <c>ddd</c> by default.</value>
        [Parameter]
        public string HeaderFormat { get; set; } = "ddd";
        [Parameter]
        public int MaxAppointmentsInSlot { get; set; } = 3;

        /// <summary>
        /// Specifies the text displayed when there are more appointments in a slot than <see cref="MaxAppointmentsInSlot" />.
        /// </summary>
        /// <value>The more text. Set to <c>"+ {0} more"</c> by default.</value>
        [Parameter]
        public string MoreText { get; set; } = "+ {0} more";
        /// <inheritdoc />
        public override DateTime StartDate
        {
            get
            {
                return Scheduler.CurrentDate.Date.AddDays(-Scheduler.CurrentDate.DayOfYear+1).Date;
            }
        }

        /// <inheritdoc />
        public override DateTime EndDate
        {
            get
            {
                return Scheduler.CurrentDate.Date.AddDays(-Scheduler.CurrentDate.DayOfYear+1).AddYears(1).AddDays(-1).Date;
            }
        }
        /// <inheritdoc />
        public override DateTime Next()
        {
            return Scheduler.CurrentDate.Date.AddDays(-Scheduler.CurrentDate.DayOfYear + 1).Date.AddYears(1);
        }

        /// <inheritdoc />
        public override DateTime Prev()
        {
            return Scheduler.CurrentDate.Date.AddDays(-Scheduler.CurrentDate.DayOfYear + 1).Date.AddYears(-1);
        }
    }
}
