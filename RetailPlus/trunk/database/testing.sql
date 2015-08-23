



SELECT ProductCode, Quantity, ReservedQuantity
FROM tblProducts prd
INNER JOIN tblProductInventory inv ON prd.ProductID = inv.ProductID
WHERE ReservedQuantity <> 0;

UPDATE tblProductInventory SET ReservedQuantity = 0 WHERE ReservedQuantity <> 0 AND ProductID > 10;

+----------------------------+-----------+------------------+
| ProductCode                | Quantity  | ReservedQuantity |
+----------------------------+-----------+------------------+
| Fundador Brandy 1.75L      |    34.000 |           -1.000 |
| JW Black 1L                |   118.000 |           -1.000 |
| Cobra Energy Drink 350ml   |    24.000 |            3.000 |
| BEAR BRAND 33g Swak Pack   |   114.000 |           -1.000 |
| Sisters Pantyliner 8s      |     0.000 |           -1.000 |
| Fundador GOLD 700ml        |    36.000 |           -8.000 |
| Cobra Defense 350ml        |    20.000 |            2.000 |
+----------------------------+-----------+------------------+

+-------------------------+---------------+------------------+
| ProductCode             | Quantity      | ReservedQuantity |
+-------------------------+---------------+------------------+
| NSM5                    |        21.000 |            4.000 |
| UAP-PRO - 3             |        17.000 |           -2.000 |
| DS-2CE15A2N-IRP (3.6mm) |        98.000 |           -7.000 |
| DS-2CE15A2N-VFIR3       |         0.000 |            1.000 |
| DS-2CE55A2N-IRP (3.6mm) |        51.000 |           -5.000 |
| IPC-721                 |        19.000 |            1.000 |
| IC-M24                  |        35.000 |            5.000 |
| IC-V80 #50              |       288.000 |           -1.000 |
| RCA                     |         0.000 |            1.000 |
+-------------------------+---------------+------------------+

SELECT * FROM sysauditlogs where Activity = 'System Login';

select trx.*
FROM tblTransactions trx
						LEFT OUTER JOIN tblTerminalReport tr ON trx.BranchID = tr.BranchID AND trx.TerminalNo = tr.TerminalNo
						LEFT OUTER JOIN (
							SELECT BranchID, TerminalNo, TransactionID, SUM(AMOUNT) AMOUNT, GROUP_CONCAT(ChequeNo) ChequeNo, MAX(ValidityDate) ValidityDate
							FROM tblChequePayment 
							WHERE TransactionID = 391
							GROUP BY BranchID, TerminalNo, TransactionID
						) tblChequePayment chque ON chque.BranchID = trx.BranchID AND chque.TerminalNo = trx.TerminalNo AND trx.TransactionID = chque.TransactionID
						where trx.transactionno = 271;



