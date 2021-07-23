--select a.QuarterDesc, b.PhaseName from v_FiscalCalendar a join phase b on a.BDate = b.StartDate and a.EDate = b.EndDate order by a.BeginDate


update phase set PhaseName = a.QuarterDesc from 
v_FiscalCalendar a join phase b on a.BDate = b.StartDate and a.EDate = b.EndDate