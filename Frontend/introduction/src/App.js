import './App.css';
import ClubTable from './ClubTable';
import { useState, useEffect } from 'react';
import axios from 'axios';
import Paging from './Paging';
import Sorting from './Sorting';
import { Link } from 'react-router-dom';

function App() {
  const [list, setList] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalClubs, setTotalClubs] = useState(0);
  const [sortingBy, setSortingBy] = useState('Name');
  const [sortOrder, setSortOrder] = useState('asc');
  const pageSize = 3;
  const totalPages = Math.ceil(totalClubs / pageSize);
  const [searchItem, setSearchItem] = useState('');
  const [membersFrom, setMembersFrom] = useState();
  const [membersTo, setMembersTo] = useState();

  useEffect(() => {
    getClubs();
    countClubs();
  }, [list, pageNumber, sortingBy, sortOrder, searchItem, membersFrom, membersTo]);


  async function getClubs(){
    try {
      const response = await axios.get("https://localhost:7056/api/Club", {
        params: {
          pageNumber: pageNumber,
          orderBy: sortingBy,
          orderDirection: sortOrder,
          name: searchItem,
          membersFrom: membersFrom,
          membersTo: membersTo
        }
      });
        setList(response.data); 
    } catch (error) {
        console.error("Error fetching clubs: ", error);
    }
  }

  async function countClubs(){
    try {
      const response = await axios.get("https://localhost:7056/api/Club/Count", {
        params: { name: searchItem, membersFrom: membersFrom, membersTo: membersTo }
      });
      setTotalClubs(response.data);
      if (response.data === 0) {
        setPageNumber(1); 
      }
    } catch (error) {
      console.error("Error fetching clubs: ", error);
    }
  }


  useEffect(() => {
    if (list.length > 0) {
      localStorage.setItem('clubs', JSON.stringify(list));
    }
  }, [list]);

  const handlePaging = (pageNumber) => {
    setPageNumber (pageNumber);
  };

  const handleSorting = (sortingBy) => {
    setSortingBy (sortingBy);
  };

  const handleSortChange = () => {
    setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
  };

  async function deleteClub(clubId){
    try{
      const response = await axios.delete("https://localhost:7056/api/Club" + `/${clubId}`);
      if(response.status === 200){
          console.log("Successfully deleted.");
      }
    }
    catch(error){
        alert(error.message);
    }
  }

  return (
    <div>
      <h1>Club List</h1>
      <Link to="/addClub">
        <button>Add New Club</button>
      </Link>
      <div>
        <input type="text" value={searchItem} onChange={(e) => {setSearchItem(e.target.value); setPageNumber(1);}} style={{width:'12em'}} placeholder='Type to search'/>
        <input type="number" min="0" value={membersFrom} onChange={(e) => setMembersFrom(e.target.value)} style={{width:'12em'}} placeholder='Members from:'/>
        <input type="number" min="0" value={membersTo} onChange={(e) => setMembersTo(e.target.value)} style={{width:'12em'}} placeholder='Members to:'/>
        <Sorting sortingBy={sortingBy} handleSorting={handleSorting} sortOrder={sortOrder} handleSortChange={handleSortChange}/>
      </div>
      <Paging pageNumber={pageNumber} totalPages={totalPages} handlePaging={handlePaging}/>
      <ClubTable clubs={list} deleteClub={deleteClub} />
    </div>
  );
}

export default App;
