import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';

const MeetingList = () => {
  const [meetings, setMeetings] = useState([]);

  useEffect(() => {
    axios.get('/api/Meetings')
      .then(response => setMeetings(response.data))
      .catch(error => console.error('Error fetching meetings:', error));
  }, []);

  return (
    <div>
      <h1>Meetings</h1>
      <Link to="/new-meeting">Create New Meeting</Link>
      <ul>
        {meetings.map(meeting => (
          <li key={meeting.meetingId}>
            <Link to={`/meetings/${meeting.meetingId}`}>{meeting.date}</Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default MeetingList;
