import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const MeetingForm = () => {
  const [meetingTypeId, setMeetingTypeId] = useState('');
  const [date, setDate] = useState('');
  const [time, setTime] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await axios.post('/api/Meetings/', { meetingTypeId, date, time });
      navigate('/meetingList');
    } catch (error) {
      console.error('Error creating meeting:', error);
    }
  };

  return (
    <div>
      <h1>Create New Meeting</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Meeting Type ID:</label>
          <input type="number" value={meetingTypeId} onChange={(e) => setMeetingTypeId(e.target.value)} required />
        </div>
        <div>
          <label>Date:</label>
          <input type="datetime-local" value={date} onChange={(e) => setDate(e.target.value)} required />
        </div>
        <div>
          <label>Time:</label>
          <input type="time" value={time} onChange={(e) => setTime(e.target.value)} required />
        </div>
        <button type="submit">Create</button>
      </form>
    </div>
  );
};

export default MeetingForm;
