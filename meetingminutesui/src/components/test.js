import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const MeetingLookups = () => {
    const { meetingId } = useParams();
    const [meeting, setMeeting] = useState([]);

    useEffect(() => {
        // Fetch meeting 
        axios.get(`/api/meetings/getMeeting/${meetingId}`)
            .then(response => {
                setMeeting(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the meeting types!', error);
            });
    }, []);

    return (
        <main>
        <div>
            <h2>Meeting Details</h2>
            <div>
                <p><strong>Meeting Type:</strong> {meeting.meetingType}</p>
                <p><strong>Date:</strong> {meeting.date}</p>
                <p><strong>Time:</strong> {meeting.time}</p>
            </div>
            {/* <div>
                <h3>Meeting Items</h3>
                {meeting.meetingItems.map((item, index) => (
                    <div key={index}>
                        <p><strong>Description:</strong> {item.description}</p>
                        <p><strong>Due Date:</strong> {item.dueDate}</p>
                        <p><strong>Person Responsible:</strong> {item.personResponsible}</p>
                        <p><strong>Status:</strong> {item.meetingItemStatus.status}</p>
                    </div>
                ))}
            </div> */}
        </div>
    </main>
        // <div>
        //     <h1>Meeting</h1>
        //     <ul>
        //         {meeting.map(type => (
        //             <li key={type.id}>{type.name}</li>
        //         ))}
        //     </ul>
        // </div>
    );
};

export default MeetingLookups;