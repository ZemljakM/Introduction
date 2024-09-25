
function Sorting({ sortingBy, handleSorting }) {
    return (
        <div className="sorting">
            <label>Sort by:</label>
            <select id="sorting" value={sortingBy} onChange={(e) => handleSorting(e.target.value)}>
                <option value="Name">Name</option>
                <option value="DateOfEstablishment">Date of Establishment</option>
                <option value="NumberOfMembers">Number of Members</option>
            </select>
        </div>
    );
}

export default Sorting;