# Mermaid ERD

```mermaid
erDiagram MEETING { int MeetingId PK int MeetingTypeId FK date Date time Time } MEETINGITEM { int MeetingItemId PK int MeetingId FK string Description date DueDate int ResponsiblePersonId FK int MeetingStatusId FK } MEETINGSTATUS { int MeetingStatusId PK string Status } MEETINGTYPE { int MeetingTypeId PK string Name } MEETING ||--o{ MEETINGITEM: has MEETINGITEM }o--|| MEETINGSTATUS: "has status" MEETING }o--|| MEETINGTYPE: "is of type"
