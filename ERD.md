# Mermaid ERD

```mermaid
erDiagram
    MeetingType {
        int MeetingTypeId
        string Name
    }
    
    Meeting {
        int MeetingId
        int MeetingTypeId
        date Date
        time Time
    }
    
    MeetingItem {
        int MeetingItemId
        int MeetingId
        string Description
        date DueDate
    }
    
    MeetingItemStatus {
        int StatusId
        int MeetingItemId
        int MeetingId
        string Status
        int ResponsiblePersonId
    }
    
    Person {
        int PersonId
        string Name
    }
    
    MeetingType ||--o{ Meeting : "has"
    Meeting ||--o{ MeetingItem : "contains"
    MeetingItem ||--o{ MeetingItemStatus : "has"
    Person ||--o{ MeetingItemStatus : "responsible for"
