/****** Script for SelectTopNRows command from SSMS  ******/
SELECT * FROM (SELECT *   FROM [younitecTestDb].[dbo].[E11-E600DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-E600DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P000DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P000DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P001DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P001DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P002DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P002DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P003DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P003DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P004DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P004DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P005DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P005DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P006DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P006DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P007DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P007DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P008DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P008DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P009DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P009DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P010DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P010DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P011DI01]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E11-P011DI02]
  union ALL SELECT *   FROM [younitecTestDb].[dbo].[E1-E600DI01]) AllUnioned
  
  WHERE [date_y] = 107 AND [date_m] = 1 AND [date_D] = 6 AND [time_h] = 13


