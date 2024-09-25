import React from 'react';
import './Paging.css'

function Pagination({ pageNumber, totalPages, handlePaging }) {
    const paginationNumbers = [];

    for (let i = 1; i <= totalPages; i++) {
        paginationNumbers.push(i);
    }

    return (
        <div className='pagination'>
            <button onClick={() => handlePaging(Math.max(1, pageNumber - 1))} disabled={pageNumber === 1}>
                &#9664; 
            </button>

            <span>
                Page {pageNumber} of {totalPages}
            </span>

            <button onClick={() => handlePaging(Math.min(totalPages, pageNumber + 1))} disabled={pageNumber === totalPages}>
                &#9654; 
            </button>

        </div>
    );
}

export default Pagination;
