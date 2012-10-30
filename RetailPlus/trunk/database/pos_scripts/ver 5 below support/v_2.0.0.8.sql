 /*********************************  v_2.0.0.8.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.8';


ALTER TABLE tblCashPayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblChequePayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblCreditCardPayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblCreditPayment ADD TransactionNo VARCHAR(30);
ALTER TABLE tblDebitPayment ADD TransactionNo VARCHAR(30);

ALTER TABLE tblCashPayment MODIFY Remarks VARCHAR(255);
ALTER TABLE tblChequePayment MODIFY Remarks VARCHAR(255);
ALTER TABLE tblCreditPayment MODIFY Remarks VARCHAR(255);
ALTER TABLE tblDebitPayment MODIFY Remarks VARCHAR(255);
       
CALL procCreditPaymentSyncTransactionNo();

/*********************************  v_2.0.0.8.sql END  *******************************************************/  