// src/App.js — replace everything with this
import { useState } from 'react';
import TransactionList from './components/TransactionList';

function App() {
    return (
        <div style={{ padding: '20px', fontFamily: 'Arial' }}>
            <h1>💳 BankTask Pro</h1>
            <TransactionList />
        </div>
    );
}

export default App;