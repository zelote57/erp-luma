SELECT ReferenceNo, PostingDate, CONCAT(ReferenceNo,PostingDate) PostingReference FROM tblInventory WHERE ReferenceNo LIKE 'CINV-%'
GROUP BY ReferenceNo, PostingDate ORDER BY ReferenceNo 