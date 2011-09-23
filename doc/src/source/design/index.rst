Design Overview
###############

Direct OnTime design is heavily influenzed by the enterprise application integration patterns (EIP). EIP provides a standards based approch to the design process and artifacts. Moreover the complexity of the design is well managed by utilizing the industry proven design pattern/solutions.

The following are the major components in the overall application design.
Message Oriented Middleware
===========================
Message Oriented Middleware acts as the major backbone for orhestration of the business process. The MOM; in this case; consists of the following elements.

Enterprise Service Bus (ESB)
----------------------------
ESB's act as the major conduit for the orchestration of the long running business processes. 

Message Queue
-------------
Message Queue (RabbitMQ) act as the responsible party for the guranteed delivery of messages in asynchronus fashion between system components. 

Web Services (SOAP 1.1/1.2)
---------------------------
Web Services provides a standard based interface to interact with the legacy systems.

