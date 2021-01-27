SELECT * FROM Reserva

SELECT * FROM Lugar

SELECT * FROM Parque

--Lugares Disponiveis em certa data
DECLARE @DataHoraInicio Datetime
SET @DataHoraInicio = '2021-01-28 00:00:00'
DECLARE @DataHoraFim Datetime
SET @DataHoraFim = '2021-01-28 11:00:00'
Select LugarID, fila, Sector, Preço from Lugar where LugarID not in (SELECT Reserva.LugarID FROM Reserva
where ((DataInicio >= @DataHoraInicio and DataFim <= @DataHoraFim) or
(DataInicio < @DataHoraInicio and @DataHoraFim between DataInicio and DataFim)) or
(DataFim > @DataHoraFim and DataInicio between @DataHoraInicio and @DataHoraFim))




select * from Reserva
where DataInicio >= @DataHoraInicio and DataFim <= @DataHoraFim

WHERE (DataInicio > @DataHoraInicio AND DataFim < @DataHoraFim) OR (DataInicio > @DataHoraFim AND DataFim < @DataHoraFim) 
AND (Lugar.LugarID = Reserva.LugarID)

--SELECT * FROM Reserva
--INNER JOIN Lugar l ON l.LugarID = Reserva.LugarID

WHERE DataFim < @DataHoraInicio OR DataInicio > @DataHoraInicio

WHERE DataFim <= 