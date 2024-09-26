import ClubRow from './ClubRow';
import './ClubTable.css';

function ClubTable({ clubs, deleteClub}) {
    return (
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Sport</th>
            <th>Date of establishment</th>
            <th>Members</th>
            <th>President</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>{clubs.map((club) => (<ClubRow key={club.id} club={club} deleteClub={deleteClub}/>))}</tbody>
      </table>
    );
  }


export default ClubTable;