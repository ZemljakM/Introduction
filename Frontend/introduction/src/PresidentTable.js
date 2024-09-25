import PresidentRow from './PresidentRow';
import './ClubTable.css';

function PresidentTable({ presidents }) {
    return (
      <table>
        <thead>
          <tr>
            <th>First name</th>
            <th>Last name</th>
          </tr>
        </thead>
        <tbody>{presidents.map((president) => (<PresidentRow key={president.id} president={president}/>))}</tbody>
      </table>
    );
  }


export default PresidentTable;