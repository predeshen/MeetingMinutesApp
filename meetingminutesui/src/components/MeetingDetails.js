import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';

const MeetingDetails = () => {
  const { meetingId } = useParams();
  const [meeting, setMeeting] = useState(null);

  useEffect(() => {
    axios.get(`/api/Meetings/${meetingId}`)
      .then(response => setMeeting(response.data))
      .catch(error => console.error('Error fetching meeting details:', error));
  }, [meetingId]);

  if (!meeting) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h1>Meeting Details</h1>
      <p>Date: {meeting.date}</p>
      <p>Time: {meeting.time}</p>
      <h2>Items</h2>
      <ul>
        {meeting.items.map(item => (
          <li key={item.meetingItemId}>
            {item.description} - {item.status}
            <Link to={`/meetings/${meetingId}/items/${item.meetingItemId}/edit`}>Edit</Link>
            <Link to={`/meetings/${meetingId}/items/${item.meetingItemId}/status`}>Update Status</Link>
          </li>
        ))}
      </ul>
      <Link to={`/meetings/${meetingId}/items/new`}>Add New Item</Link>
    </div>
  );
};

export default MeetingDetails;
