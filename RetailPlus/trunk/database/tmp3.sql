

-- CALL procContactCreditPaymentSelect(0, '', 9027, 0, 0, '2014-10-22', '2014-12-21', '', '', '', '', '', '', 0);



SELECT trx.BranchID, trx.TerminalNo, trx.SyncID, 0 CreditPaymentCashID, trx.TransactionID, trx.TransactionNo, trx.CreatedOn TransactionDate,
                                                      0 LatePenaltyAmount,
                                                      0 FinanceChargeAmount,
                                                      SubTotal PrincipalAmount,
                                                      '' Remarks, trx.CreatedOn, trx.LastModified,
                                                      'Cash' AS PaymentSource,
                                                      trx.BranchID AS CPRefBranchID, trx.TerminalNo AS CPRefTerminalNo, trx.TransactionNo AS CPRefTransactionNo,
                                                      0 CreditReasonID, CONCAT('Payment @ Ter#:', trx.TerminalNo,' Br#:',trx.BranchID) CreditReason,
                                                      cci.CreditCardNo, cntct.ContactName, trx.SubTotal Amount,
                                                      IFNULL(gci.CreditCardNo, '') GuarantorCreditCardNo,
                                                      IFNULL(gua.ContactCode, '') GuarantorCode,
                                                      IFNULL(gua.ContactName,'') GuarantorName
                                              FROM tblTransactions trx
                                              INNER JOIN tblContactCreditCardInfo cci ON cci.CustomerID = trx.CustomerID
                                              INNER JOIN tblContacts cntct ON cntct.ContactID =  trx.CustomerID
                                              LEFT OUTER JOIN tblContactCreditCardInfo gci ON gci.CustomerID = cci.GuarantorID
                                              LEFT OUTER JOIN tblContacts gua ON gua.ContactID = cci.GuarantorID
                                              WHERE TransactionStatus = 7 
												AND trx.CustomerID = 9027 
												AND cci.GuarantorID <> 0 
												AND trx.CreatedOn >= '2014-10-22 00:00:00' 
												AND trx.CreatedOn <= '2014-12-21 00:00:00' 
											  ORDER BY trx.CreatedOn;
SELECT CONVERT(Transactiondate, DATE), CONVERT(CreatedOn, DATE), count(Transactiondate) 
FROM tblTransactions a WHERE CONVERT(CreatedOn, DATE) = '1900-01-01'
GROUP BY CONVERT(Transactiondate, DATE), CONVERT(CreatedOn, DATE);

SELECT Transactiondate, createdon, LastModified, a.* FROM tblTransactions a WHERE CONVERT(CreatedOn, DATE) = '1900-01-01' limit 10;

UPDATE tblcontactcreditcardinfo set Last2BillingDate = '2014-12-06' where Last2BillingDate = '2014-12-20';
UPDATE tblTransactions SET CreatedOn = TransactionDate WHERE CONVERT(CreatedOn, DATE) = '1900-01-01';
UPDATE tblTransactions SET LastModified = TransactionDate WHERE CONVERT(LastModified, DATE) = '1900-01-01';
