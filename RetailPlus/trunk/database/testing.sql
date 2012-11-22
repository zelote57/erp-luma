
update tblContactRewards set RewardActive = 0 
where DATE_FORMAT(RewardAwardDate, '%Y-%m-%d') = '2012-01-07'
limit 2;


SELECT 
	CALD.CalDate RewardAwardDate
	,SUM(IF(RewardActive=0,1,0)) NoOfInActiveRewards
	,SUM(IF(RewardActive=1,1,0)) NoOfActiveRewards
FROM tblCalDate CALD
LEFT OUTER JOIN tblContactRewards CREW ON CALD.CalDate = DATE_FORMAT(CREW.RewardAwardDate, '%Y-%m-%d')
WHERE 
	CALD.CalDate BETWEEN DATE_FORMAT('2012-01-01', '%Y-%m-%d')  AND 
			DATE_FORMAT('2012-02-12', '%Y-%m-%d') 
GROUP BY 
	CALD.CalDate
ORDER BY CALD.CalDate;

-- select * from tblContactRewards limit 10;

SELECT 
	DATE_FORMAT(CREW.RewardAwardDate, '%Y-%m-%d')
	,SUM(IF(RewardActive=0,1,0)) RewardInActive
	,SUM(IF(RewardActive=1,1,0)) RewardActive
FROM tblContactRewards CREW 
WHERE 
	RewardAwardDate BETWEEN DATE_ADD(NOW(), INTERVAL -360 DAY) AND NOW()
GROUP BY 
	DATE_FORMAT(CREW.RewardAwardDate, '%Y-%m-%d')
ORDER BY DATE_FORMAT(CREW.RewardAwardDate, '%Y-%m-%d');
