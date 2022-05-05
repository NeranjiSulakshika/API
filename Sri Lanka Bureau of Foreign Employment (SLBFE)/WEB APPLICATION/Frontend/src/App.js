import './App.css'
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import SignUp from './components/SignUp'
import SignIn from './components/SignIn'
import AdminSignIn from './components/AdminSignIn'
import HomePage from './components/HomePage'

function App() {
  return (
    <div className="App">
      <Router>
        <Switch>
          <Route exact path="/" component={SignUp} />
          <Route exact path="/SignIn" component={SignIn} />
          <Route exact path="/AdminSignIn" component={AdminSignIn} />
          <Route exact path="/HomePage" component={HomePage} />
        </Switch>
      </Router>
    </div>
  )
}

export default App
