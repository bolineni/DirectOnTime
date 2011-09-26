Design Overview
###############

Direct OnTime design is heavily influenzed by the enterprise application integration patterns (EIP). EIP provides a standards based approch to the design, process and artifacts. Moreover the complexity of the design is well managed by utilizing the industry proven design pattern/solutions.

One of the major open source components utilized in the design & development of Direct OnTime solution is MassTransit among other opensource projects.

Why MassTransit?
================
'MassTransit <http://masstransit-project.com/>'_ is a lean framework for creating distributed applications on the .Net platform. MassTransit provides the ability to subscribe messages by type and then connect different processing nodes through messaging at the same time building mesh of services.

MassTransit designed to be used inside the firewall, and not as a means to communicate with the external vendors or interfaces. Because of this, it becomes a great choice to build and orchestrate the internal business, at the same time ahering to the great principles of EIP.

MassTransit available for .Net 3.5 & 4.0. Our choice of implementation will be .Net 4.0.

Other Technology Standards
==========================

Web Serices (SOAP 1.1/1.2)
--------------------------
Web Service is a method of communication between to application interfaces over the network. Each Web Service expose an interface described in a multi-platform processable format called as WSDL. Other applications interact with the service in a manner described by WSDL.

Web Services can be used to support various integration strategies in various styles. Under this application desgin we will follow the SOA concepts where the web services interact using the pre-defined and published message sets.

Message Oriented Middleware / Message Broker
--------------------------------------------
Message Oriented Middleware acts as the backbone for orhestration of the business process with in Direct OnTime system. MOM allows the integration of business processes running in hetrogenous application platforms. MOM of our choice is RabbitMQ which is an open source one using the standard AMQP protocol.

Design Footnotes
================

Designing Message(s)
--------------------
<From MT Docs> Message design shouldnt be looked at from a OO prespective. Messages are more about contracts between application interfaces. With that option in mind, it is better to have messages implement interface rather than having a big bloated base class. Interface based design will also help to evolve the design and implementation of messages. </From MT Docs>

In Message Oriented Design, messages are all about the state and not about behaviour. With that in mind, it is better to design of messages be lean and carry only the required information for interaction. Having a fatty and bloated messages will prompt for content based routing which is a bad design practice.