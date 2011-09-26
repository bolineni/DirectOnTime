Design Overview
###############

Direct OnTime design is heavily influenzed by the enterprise application integration patterns (EIP). EIP provides a standards based approch to the design, process and artifacts. Moreover the complexity of the design is well managed by utilizing the industry proven design pattern/solutions.

One of the major open source components utilized in the design & development of Direct OnTime solution is MassTransit among other opensource projects.

Why MassTransit?
================
`MassTransit <http://masstransit-project.com/>`_ is a lean framework for creating distributed applications on the .Net platform. MassTransit provides the ability to subscribe messages by type and then connect different processing nodes through messaging at the same time building mesh of services.

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
Message Oriented Middleware acts as the backbone for orhestration of the business process with in Direct OnTime system. MOM allows the integration of business processes running in hetrogenous application platforms. MOM of our choice is `RabbitMQ <http://www.rabbitmq.com/>`_ which is an open source one using the standard AMQP protocol.

Design Footnotes
================

Designing Message(s)
--------------------
<From MT Docs> Message design shouldnt be looked at from a OO prespective. Messages are more about contracts between application interfaces. With that option in mind, it is better to have messages implement interface rather than having a big bloated base class. Interface based design will also help to evolve the design and implementation of messages. </From MT Docs>

In Message Oriented Design, messages are all about the state and not about behaviour. With that in mind, it is better to design of messages be lean and carry only the required information for interaction. Having a fatty and bloated messages will prompt for content based routing which is a bad design practice.

Designing Application Interface(s)
----------------------------------
Message Oriented Integration works effectively when we can design the application process interfaces as granular as possible. This process will involve more integration points than a having a big application interface which does more than one job. Having a more granular application design promoted reuse and also helps in restarting the application processing. In any integration project which uses messaging, it is better to have more messages to orchestrate the business process than a very limitted one.

Saga design should be done with care. It is better to have a saga be in a wait state for more than it is meaningfully required. For example if your business process takes more than a day to complete, try to see whether the business process can be divided in to two different integration tasks rather than two steps in the same saga. Saga's are clearly meant for coordinating long running processes but long is always a question. In my point of view if the process run takes more than 12 hours to finish, then better divide in to multiple integration processes rather than looking at as two different steps.

Direct OnTime - Application Design
----------------------------------
The following sections shall describe the general priciples, functions and responsibilities of various elements within Direct OnTime application stack.

.. toctree::

    Process Coordinator.rst
	Monthly Payment Processor.rst
	New Business Processor.rst






