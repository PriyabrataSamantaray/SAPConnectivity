/*
Update AuditPlan Data for new fiscal calendar
*/

update planAudit set period = 'FY18-Q2' where period = 'FY17-Q4'
update planAudit set period = 'FY18-Q1' where period = 'FY17-Q3'
update planAudit set period = 'FY17-Q4' where period = 'FY17-Q2'
update planAudit set period = 'FY17-Q3' where period = 'FY17-Q1'
update planAudit set period = 'FY17-Q2' where period = 'FY16-Q4'
update planAudit set period = 'FY17-Q1' where period = 'FY16-Q3'
update planAudit set period = 'FY16-Q4' where period = 'FY16-Q2'
update planAudit set period = 'FY16-Q3' where period = 'FY16-Q1'
update planAudit set period = 'FY16-Q2' where period = 'FY15-Q4'
update planAudit set period = 'FY16-Q1' where period = 'FY15-Q3'
