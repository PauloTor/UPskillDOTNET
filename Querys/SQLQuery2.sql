SELECT * FROM Reserva

SELECT * FROM Reserva
WHERE DataReserva = '2020-01-05'

SELECT * FROM Reserva
WHERE DataInicio BETWEEN '2020-01-05 14:00:00' AND '2020-01-05 16:30:00'

SELECT * FROM	Lugar, Reserva
WHERE DataReserva = '2020-01-07' AND Lugar.LugarID = Reserva.LugarID

DECLARE @DataHoraInicio datetime
SET @DataHoraInicio = '2020-01-07 15:00:00'
DECLARE @DataHoraFim datetime
SET @DataHoraFim = '2020-01-07 16:00:00'
SELECT DISTINCT * FROM Lugar, Reserva
WHERE DataInicio < @DataHoraInicio AND DataFim < @DataHoraInicio OR DataInicio > @DataHoraFim AND DataFim > @DataHoraFim 
AND Lugar.LugarID = Reserva.LugarID

SELECT * FROM Reserva
INNER JOIN Lugar l ON l.LugarID = Reserva.LugarID

