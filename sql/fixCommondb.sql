update t_FiscalDays set CalDate = CalDate + 2 where FiscalID >= 20120728
update t_FiscalDays set FiscalID = DATEPART(YYYY, CalDate) * 10000 + DATEPART(MM, caldate) * 100 + DATEPART(DD, caldate)
delete t_FiscalDays where CalDate >= '2014-10-27'

update t_FiscalDays set FiscalPeriod = 10 where CalDate >= '2012-07-30' and CalDate <= '2012-08-26'
update t_FiscalDays set FiscalPeriod = 11 where CalDate >= '2012-08-27' and CalDate <= '2012-09-23'
update t_FiscalDays set FiscalPeriod = 12 where CalDate >= '2012-09-24' and CalDate <= '2012-10-28'

update t_FiscalDays set FiscalPeriod = 1  where CalDate >= '2012-10-29' and CalDate <= '2012-11-25'
update t_FiscalDays set FiscalPeriod = 2 where CalDate >= '2012-11-26' and CalDate <= '2012-12-23'
update t_FiscalDays set FiscalPeriod = 3 where CalDate >= '2012-12-24' and CalDate <= '2013-1-27'

update t_FiscalDays set FiscalPeriod = 4  where CalDate >= '2013-1-28' and CalDate <= '2013-2-24'
update t_FiscalDays set FiscalPeriod = 5 where CalDate >= '2013-2-25' and CalDate <= '2013-3-24'
update t_FiscalDays set FiscalPeriod = 6 where CalDate >= '2013-3-25' and CalDate <= '2013-4-28'

update t_FiscalDays set FiscalPeriod = 7  where CalDate >= '2013-4-29' and CalDate <= '2013-5-26'
update t_FiscalDays set FiscalPeriod = 8 where CalDate >= '2013-5-27' and CalDate <= '2013-6-23'
update t_FiscalDays set FiscalPeriod = 9 where CalDate >= '2013-6-24' and CalDate <= '2013-7-28'

update t_FiscalDays set FiscalPeriod = 10  where CalDate >= '2013-7-29' and CalDate <= '2013-8-25'
update t_FiscalDays set FiscalPeriod = 11 where CalDate >= '2013-8-26' and CalDate <= '2013-9-22'
update t_FiscalDays set FiscalPeriod = 12 where CalDate >= '2013-9-23' and CalDate <= '2013-10-27'


update t_FiscalDays set FiscalPeriod = 1  where CalDate >= '2013-10-28' and CalDate <= '2013-11-24'
update t_FiscalDays set FiscalPeriod = 2 where CalDate >= '2013-11-25' and CalDate <= '2013-12-22'
update t_FiscalDays set FiscalPeriod = 3 where CalDate >= '2013-12-23' and CalDate <= '2014-1-26'

update t_FiscalDays set FiscalPeriod = 4  where CalDate >= '2014-1-27' and CalDate <= '2014-2-23'
update t_FiscalDays set FiscalPeriod = 5 where CalDate >= '2014-2-24' and CalDate <= '2014-3-23'
update t_FiscalDays set FiscalPeriod = 6 where CalDate >= '2014-3-24' and CalDate <= '2014-4-27'

update t_FiscalDays set FiscalPeriod = 7  where CalDate >= '2014-4-28' and CalDate <= '2014-5-25'
update t_FiscalDays set FiscalPeriod = 8 where CalDate >= '2014-5-26' and CalDate <= '2014-6-22'
update t_FiscalDays set FiscalPeriod = 9 where CalDate >= '2014-6-23' and CalDate <= '2014-7-27'

update t_FiscalDays set FiscalPeriod = 10  where CalDate >= '2014-7-28' and CalDate <= '2014-8-24'
update t_FiscalDays set FiscalPeriod = 11 where CalDate >= '2014-8-25' and CalDate <= '2014-9-21'
update t_FiscalDays set FiscalPeriod = 12 where CalDate >= '2014-9-22' and CalDate <= '2014-10-26'