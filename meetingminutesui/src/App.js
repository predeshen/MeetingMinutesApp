// src/App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import MeetingList from './components/MeetingList';
import MeetingForm from './components/MeetingForm';
import MeetingDetails from './components/MeetingDetails';
import MeetingItemForm from './components/MeetingItemForm';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MeetingList />} />
        <Route path="/new-meeting" element={<MeetingForm />} />
        <Route path="/meetings/:meetingId" element={<MeetingDetails />} />
        <Route path="/meetings/:meetingId/items/new" element={<MeetingItemForm />} />
        {/* Add routes for editing and updating status */}
      </Routes>
    </Router>
  );
};

export default App;
