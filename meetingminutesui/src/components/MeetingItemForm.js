import React, { useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

const MeetingItemForm = () => {
  const { meetingId } = useParams();
  const [description, setDescription] = useState('');
  const [dueDate, setDueDate] = useState('');
  const [responsiblePersonId, setResponsiblePersonId] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await axios.post(`/api/Meetings/${meetingId}/items`, { description, dueDate, responsiblePersonId });
      navigate(`/meetings/${meetingId}`);
    } catch (error) {
      console.error('Error adding meeting item:', error);
    }
  };

  return (
    <div>
      <h1>Add Meeting Item</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Description:</label>
          <input type="text" value={description} onChange={(e) => setDescription(e.target.value)} required />
        </div>
        <div>
          <label>Due Date:</label>
          <input type="date" value={dueDate} onChange={(e) => setDueDate(e.target.value)} required />
        </div>
        <div>
          <label>Responsible Person ID:</label>
          <input type="number" value={responsiblePersonId} onChange={(e) => setResponsiblePersonId(e.target.value)} required />
        </div>
        <button type="submit">Add</button>
      </form>
    </div>
  );
};

export default MeetingItemForm;
