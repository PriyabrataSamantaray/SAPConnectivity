--select a.QuarterDesc, b.PhaseName from v_FiscalCalendar a join phase b on a.BDate = b.StartDate and a.EDate = b.EndDate order by a.BeginDate


--update phase set PhaseName = a.QuarterDesc from 
--v_FiscalCalendar a join phase b on a.BDate = b.StartDate and a.EDate = b.EndDate

update phase set phaseName = 'FY18-Q2' where phaseName = 'FY17-Q4'
update phase set phaseName = 'FY18-Q1' where phaseName = 'FY17-Q3'
update phase set phaseName = 'FY17-Q4' where phaseName = 'FY17-Q2'
update phase set phaseName = 'FY17-Q3' where phaseName = 'FY17-Q1'
update phase set phaseName = 'FY17-Q2' where phaseName = 'FY16-Q4'
update phase set phaseName = 'FY17-Q1' where phaseName = 'FY16-Q3'
update phase set phaseName = 'FY16-Q4' where phaseName = 'FY16-Q2'
update phase set phaseName = 'FY16-Q3' where phaseName = 'FY16-Q1'
update phase set phaseName = 'FY16-Q2' where phaseName = 'FY15-Q4'
update phase set phaseName = 'FY16-Q1' where phaseName = 'FY15-Q3'





update phase set StartDate = a.BDate, endDate = a.Edate from  --fix production ASAP
v_FiscalCalendar a join phase b on a.QuarterDesc = b.PhaseName




