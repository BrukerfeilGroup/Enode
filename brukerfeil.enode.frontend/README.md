This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).



In the project directory, you can run:
### `npm install`
You have to run this the first time you run the project, or after node packages have been added. This will install all the dependencies specified in `package.json` (includes React and TypeScript).

### `npm start`

Runs the app in the development mode.<br />
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.<br />
You will also see any lint errors in the console.

#### Configuring backend endpoint

Depending on your OS, the port to the backend API will change. Navigate to `src/constants.ts` and change the port in the BACKEND_BASEURL constant.

### `npm test`

Launches the test runner in the interactive watch mode.<br />
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.


## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).



<br>

# Frontend Developing Guidelines

1.  [Components](#components)
    - [Directories](#directories)
    - [Functional Components](#functional-components)
    - [Props](#props)
    - [State](#using-state)
2.  [Types](#types)
3.  [Hooks](#hooks)
4.  [Naming Conventions](#naming-conventions)
5.  [Styling](#styling)
6.  [Testing](#testing)
# 
## Components

### Directories
All components must be created inside of `src/components`.

We will create one file per component, `index.tsx`, and that file will reside in a folder which is named the same as your component. The components respective css-file will also reside in this folder and will be named `styles.module.css`. Here is an example of two container components' directory structure:
```
src/
└── components/
    └── containers
        ├── MessageTable/
        │   ├── index.tsx
        │   └── styles.module.css
        └── SomeOtherComponent/
            ├── index.tsx
            └── styles.module.css
```

Relative imports will be cleaner by naming the component-file `index.tsx` and putting it in its own directory. 


There are a few directories which we will use.
* containers - Components which perform a substantial amount of logic and contain a state.
* common - Components which will be used several times and by different components. Examples could be a navbar or a custom button.

Simpler, stateless components which focuses more on rendering will reside in `src/components`. 

### Functional Components
Make an effort to only use functional components instead of class components.

_Example of a functional component_
```javascript
const MyComponent: React.FC = () => {
    return <h1>Hello World!</h1>
}

export default MyComponent
```
_Remember to `export default` the component. This makes it possible to import the component in other files_

### Props

All components which require props should have the Props-type defined above the component. The naming convention of props is: [ComponentName]Props

```javascript
type MyComponentProps = {
    myStringProp: string,
    myNumberProp: number,
    myFunctionProp: () => void
}

const MyComponent: React.FC<MyComponentProps> = props => {
    return (
        <p 
            onClick={() => props.myFunctionProp()}
            >
            This is you string prop: {props.myStringProp}
            and this is your number prop: {props.myNumberProp}
        </p>
    )
}
```

### Using state
When using a state in your component, it should generally reside at the top of your component.

```javascript
const MyComponent: React.FC = () => {
    const [myState, setMyState] = useState<string>("Initial state")

    return (...)
}
```
## Types
As our application is using TypeScript, defining types is often needed. These types should reside in the folder `src/types`. The exception for this is component props. Generally, there should be one type per file, and this file should be named `[NameOfType].ts`, but if a type is using a subtype, this may also reside in the same file as the parent type. Here is an example of a type `Person` in `Person.ts`:

```javascript
type SomeSubType = {
    lat: string,
    lng: string
}

export type Person = {
    name: string,
    age: number,
    male: boolean,
    positions: SomeSubType[]
}
```

## Hooks
When writing our own custom hooks, we should name the file with the prefix '`use`' followed by what the hook is representing. If you are writing a hook that fetches a list of Organizations, your hook should be named `useOrganizations`. The file name should be `[hookName].ts` and should reside in the folder `src/hooks`. Example of a hook `useOrganizations` in the file `useOrganizations.ts`:

```javascript
export default () => {
    const [organizations, setOrganizations] = useState<Organization>()

    const fetchOrganizations = async () => {
        await somethingAsync()
    }

    useEffect(() => {
        fetchOrganizations()
    }, [])

    return organizations;
}
```

## Naming Conventions
### General
All things that you define, types, components, functions, states and variables, must have a logical and descriptive name. Do not name your component `OrgTab` if it is a `OrganizationTable`. An array of something should ideally be named with the plural form of the object the array represents; a list of messages shoud be named `messages`, not `msgs`.

## Styling
We are using [CSS Modules](https://create-react-app.dev/docs/adding-a-css-modules-stylesheet/). This means that each component will have their own CSS-file named `styles.module.css`. See [Directories](#Directories) for directory structure. We will be using classes only. To apply our styles, we import the CSS as an object, like this: `import styles from "./styles.module.css`, and use that object by passing it through the className prop. Example follows:

_MyComponent/styles.module.css_
```css
.container {
    display: flex;
    flex-direction: row;
    color: #FFFFFF;
}
.header {
    font-weight: 40px;
}
```
_MyComponent/index.tsx_
```javascript
import React from "react"
import styles from "./styles.module.css"

const MyComponent: React.FC = () => {
    return (
        <div className={styles.container}>
            <h1 className={styles.header}>Hello world!</h1>
        </div>
    )
}
```

## Testing
To run all tests, run `npm test`

Test files should be of this format: `[fileNameOfWhatYouAreTesting].test.ts` and should be in the same folder as the file you are writing tests for.
