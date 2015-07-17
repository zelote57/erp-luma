


SELECT mobileno, contactid, mobileno, homephoneno from tblcontactaddon 
WHERE mobileno LIKE '%F%';

SELECT mobileno, contactid, mobileno, homephoneno from tblcontactaddon 
WHERE homephoneno LIKE '%F%';

SELECT contactname, mobileno, cntct.contactid, homephoneno, telephoneno
from tblcontacts cntct 
left outer join tblcontactaddon addon on cntct.contactid = addon.contactid
WHERE mobileno <> '' AND LENGTH(mobileno) < 11 limit 10;

SELECT telephoneno, cntct.contactid, REPLACE(telephoneno, ' office', '') telno, address, concat(address, ' ',telephoneno)
from tblcontacts cntct
inner join tblcontactrewards rwd on cntct.contactid = rwd.customerid
WHERE telephoneno <> '' 
and telephoneno not LIKE '%/%'
AND telephoneno not REGEXP '^[0-9]+$' LIMIT 150;

UPDATE tblcontacts SET address = concat(address, ' ',telephoneno) WHERE telephoneno LIKE '%DAETCN%';

UPDATE tblcontacts SET telephoneno = REPLACE(telephoneno, 'F', '') WHERE telephoneno LIKE '%F%';

UPDATE tblcontacts SET telephoneno = trim(telephoneno);



