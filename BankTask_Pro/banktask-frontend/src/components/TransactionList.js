// src/components/TransactionList.js
import { useState, useEffect } from 'react';

function TransactionList() {

    // State 1: holds the transaction data from .NET API
    const [transactions, setTransactions] = useState([]);

    // State 2: are we waiting for API response?
    const [loading, setLoading] = useState(true);

    // State 3: did something go wrong?
    const [error, setError] = useState(null);

    // useEffect — runs ONCE when component appears on screen
    // This is where we call our .NET API
    useEffect(() => {

        const fetchTransactions = async () => {
            try {
                // Call your .NET API
                const response = await fetch('http://localhost:5066/api/transactions');

                // Check if API returned error
                if (!response.ok) {
                    throw new Error(`API Error: ${response.status}`);
                }

                // Convert response to JavaScript array
                const data = await response.json();

                // Store in state → triggers re-render
                setTransactions(data);

            } catch (err) {
                // Store error → triggers re-render with error UI
                setError(err.message);

            } finally {
                // Always hide loading — whether success or error
                setLoading(false);
            }
        };

        fetchTransactions();

    }, []); // [] = run once on mount only

    // Show while waiting for API
    if (loading) return <p>⏳ Loading transactions...</p>;

    // Show if API call failed
    if (error) return <p style={{ color: 'red' }}>❌ Error: {error}</p>;

    // Show when data is ready
    return (
        <div>
            <h2>Transactions ({transactions.length})</h2>

            <table border="1" cellPadding="8" style={{ borderCollapse: 'collapse' }}>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Account</th>
                        <th>Amount</th>
                        <th>Type</th>
                        <th>Category</th>
                        <th>Status</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map(txn => (
                        // key = txn.id — required for lists
                        <tr key={txn.id}>
                            <td>{txn.id}</td>
                            <td>{txn.accountId}</td>
                            <td>{txn.formattedAmount}</td>
                            <td>{txn.type}</td>
                            <td>{txn.category}</td>
                            <td>{txn.status}</td>
                            <td>{txn.date}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TransactionList;