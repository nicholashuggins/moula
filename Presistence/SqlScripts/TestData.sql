truncate table paymentrequests
truncate table [transaction]
-- an Initial Balance
INSERT INTO [transaction] values(NewID(),'TestCustomer',GetDate(),100000)
--a Pending Request
insert into paymentrequests values(NewID(),'TestCustomer',GetDate(),10,0,0,'TestCustomer0001')
--a Processed Request
insert into paymentrequests values(NewID(),'TestCustomer',GetDate(),10,1,0,'TestCustomer0002')
--a Closed Request
insert into paymentrequests values(NewID(),'TestCustomer',GetDate(),10,2,0,'TestCustomer0003')
--a Pending Request for Processing
insert into paymentrequests values(NewID(),'TestCustomer',GetDate(),10,0,0,'TestCustomer0004')

--Balance Test data
-- an Initial Balance
INSERT INTO [transaction] values(NewID(),'TestCustomerBalance',GetDate(),1000)
--2 Pending Requests
insert into paymentrequests values(NewID(),'TestCustomerBalance',GetDate(),100,0,0,'TestCustomerBalance0001')
insert into paymentrequests values(NewID(),'TestCustomerBalance',GetDate(),500,0,0,'TestCustomerBalance0002')
--a Processed Request and transaction
insert into paymentrequests values(NewID(),'TestCustomerBalance',GetDate(),100,1,0,'TestCustomerBalance0003')
INSERT INTO [transaction] values(NewID(),'TestCustomerBalance',GetDate(),-100)
--3 Closed Request
insert into paymentrequests values(NewID(),'TestCustomerBalance',GetDate(),25,2,0,'TestCustomerBalance0004')
insert into paymentrequests values(NewID(),'TestCustomerBalance',GetDate(),35,2,0,'TestCustomerBalance0005')
insert into paymentrequests values(NewID(),'TestCustomerBalance',GetDate(),45,2,0,'TestCustomerBalance0006')

