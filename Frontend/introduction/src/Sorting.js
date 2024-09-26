import Button from './Button';

function Sorting({ sortingBy, handleSorting, sortOrder, handleSortChange}) {
    return (
        <div className="sorting">
            <label>Sort by:</label>
            <select id="sorting" value={sortingBy} onChange={(e) => handleSorting(e.target.value)} style={{width:'14em'}}>
                <option value="Name">Name</option>
                <option value="DateOfEstablishment">Date of Establishment</option>
                <option value="NumberOfMembers">Number of Members</option>
            </select>
            <Button className="arrow" onClick={handleSortChange} text={sortOrder === 'asc' ? '↑' : '↓'}/>
        </div>
    );
}

export default Sorting;