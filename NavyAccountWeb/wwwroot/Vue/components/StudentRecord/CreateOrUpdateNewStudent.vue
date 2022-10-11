

<template>
    <!-- WRAPPER -->

    <div class="col-md-12">

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">?</span>
            </button>
        </div>

        <div class="col-12">
            <form id="smartwizard-validation" class="wizard wizard-primary sw-main sw-theme-default" @submit="postStudent" method="post" action="javascript:void(0)" novalidate="novalidate">
                <ul class="nav nav-tabs step-anchor">
                    <li class="nav-item active"><a href="#validation-step-1" class="nav-link">Student Details</a></li>
                    <!-- <li class="nav-item"><a href="#validation-step-2" class="nav-link">Parent</a></li> -->
                    
                </ul>

                <div class="sw-container tab-content">
                    <div id="validation-step-1" class="tab-pane step-content col-12" style="display: block;">
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Reg Number</label>
                                <input class="form-control" name="Reg_Number" v-model="postBody.student.Reg_Number" :disabled="studenttoeditid > 0" placeholder=""  required />
                            </div>
                        </div>
                            <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>SurName</label>
                                <input class="form-control" name="Surname" v-model="postBody.student.Surname" placeholder="" required />
                            </div>
                            <div class="form-group col-md-4">
                                <label>FirstName</label>
                                <input class="form-control" name="FirstName" v-model="postBody.student.FirstName" required />
                            </div>
                            <div class="form-group col-md-4">
                                <label>MiddleName</label>
                                <input class="form-control" name="MiddleName" v-model="postBody.student.MiddleName" required />
                            </div>
                            
                            </div>
                        
                        <div class="form-row">
                            <div class="col-sm-4">
                                <label>Age</label>
                                <input class="form-control" name="Age" v-model="postBody.student.Age" required />
                            </div>
                            <div class="col-12 col-xl-4">
                                <div class="form-group">
                                    <label class="form-label">Parent Name</label>
                                    <vuejsAutocomplete source="/api/ParentRecord/getAllParentByNameLimited/"
                                                    input-class="form-control"
                                                    @selected="setValueParentID"
                                                    name="parentid"
                                                    v-model="postBody.student.Parentid">
                                    </vuejsAutocomplete>
 
                                </div>
                                </div>
                                <div class="col-12 col-xl-4">
                                <div class="form-group">
                                    <label class="form-label">Guardian Name</label>
                                    <vuejsAutocomplete source="/api/ParentRecord/getAllGuardianByNameLimited/"
                                                    input-class="form-control"
                                                    @selected="setValueGuardianID"
                                                    name="guardianid"
                                                    v-model="postBody.student.Guardianid">
                                    </vuejsAutocomplete>

                                </div>
                             </div>
                               

                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Sex</label>
                                <select class="form-control" v-model="postBody.student.Sex" name="sex" required>
                                    <option v-for="ge in gender" v-bind:value="ge.value" v-bind:key="ge.value"> {{ ge.text }} </option>
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label>Email</label>
                                <input class="form-control" name="Email" v-model="postBody.student.Email" />
                            </div>
                            <div class="col-md-4">
                                <label>PhoneNumber</label>
                                <input class="form-control" name="PhoneNumber" v-model="postBody.student.PhoneNumber" />
                            </div>
                        </div>

                        <div class="form-row">
                            
                            <div class="form-group col-md-4">
                                <label>Class</label>
                                <select class="form-control" v-model="postBody.student.ClassId" name="classId" required>
                                    <option v-for="cls in classList" v-bind:value="cls.id" v-bind:key="cls.id"> {{ cls.className }} </option>
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label>School</label>
                                <select class="form-control" v-model="postBody.student.SchoolId" name="schoolId" required>
                                    <option v-for="sch in schoolList" v-bind:value="sch.id" v-bind:key="sch.id"> {{ sch.schoolname }} </option>
                                </select>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Class Category</label>
                                <input class="form-control" name="ClassCategory" v-model="postBody.student.ClassCategory" />
                            </div>
                            

                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Status</label>
                                <select class="form-control" v-model="postBody.student.Status" name="status" required>
                                    <option v-for="stu in statusList" v-bind:value="stu.value" v-bind:key="stu.value"> {{ stu.text }} </option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label>Enrolment Date</label>
                                <vuejsDatepicker v-model="postBody.student.CommencementDate" input-class="form-control" name="commencementDate"></vuejsDatepicker>
                            </div>

                             <div class="col-md-4">
                                <label>Exit Left</label>
                                <vuejsDatepicker v-model="postBody.student.ExitDate" input-class="form-control" name="cxitDate"></vuejsDatepicker>
                            </div>
                            <div class="col-md-4">
                                <label>Exit Reason</label>
                                <input class="form-control" name="exitReason" v-model="postBody.student.ExitReason" />
                            </div>
                            <!-- <div class="filter">
                             <label for="autocomplete-dropdown">Autocomplete dropdown: </label>
                             <autocomplete-dropdown id="autocomplete-dropdown" :options="fruitOptions" v-model="postBody.student.ExitReason"></autocomplete-dropdown>
                            </div> -->
                        </div>
                      
                        
                    </div>

                </div>
            </form>
        </div>

    </div>

    <!-- END WRAPPER -->
</template>


<script>
    import Axios from 'axios';
    import vuejsAutocomplete from 'vuejs-auto-complete'
    import vuejsDatepicker from 'vuejs-datepicker'
    export default {
        props:['studenttoeditid'],
        components: {
            vuejsDatepicker,
            vuejsAutocomplete,
            Axios
        },
        data() {
            return {
                parentList: null,
                classList: null,
                schoolList: null,
                errors: null,
                addbene2: false,
                addbene22: true,
                addbene3: false,
                addbene33: true,
                responseMessage: '',
                objectToClear: {},
                postBody: {
                    student:
                    {
                        Reg_Number:'',
                Surname:'',
                FirstName:'',
                MiddleName:'',
                Sex:'',
                SchoolCode:'',
                CommencementDate:'',
                Class:'',
                PhoneNumber:'',
                Email:'',
                Status:'',
                ExitDate:'',
                ExitReason:'',
                Age:'',
                ClassCategory:'',
                ParentalStatus:'',
                Parentid:null,
                Guardianid:null,
                studentid:null,
                ClassId:null,
                SchoolId:null
                        
                    },

                },
                
                gender: [
                    { value: 'M', text: 'Male' },
                    { value: 'F', text: 'Female' }
                ],
                statusList: [
                    { value: 'Active', text: 'Active' },
                    { value: 'On Claim', text: 'On Claim' },
                    { value: 'Inactive', text: 'Inactive' }
                ],
                exitreasonList: [
                    { value: 'Graduated', text: 'Graduated' },
                    { value: 'Suspended', text: 'Suspended' },
                    { value: 'Expelled', text: 'Expelled' },
                    { value: 'Death', text: 'Death' },
                    { value: 'Absconded', text: 'Absconded' }

                ]
                
            };
        },

        methods: {
            setValueParentID(result){
            this.postBody.student.Parentid=result.value;
            },
            setValueGuardianID(result){
            this.postBody.student.Guardianid=result.value;
            },
            postStudent() {
                this.addOrEdit(this.studenttoeditid)
            },
            addOrEdit(actionToPerform) {
               
                let uri = actionToPerform == 0 ? `/api/StudentRecord/CreateStudent` : `/api/StudentRecord/Update`;
                let message = actionToPerform == 0 ? `Created` : `Updated`;
                console.log(this.postBody);
                alert(this.postBody);
              
                Axios.post(uri,this.postBody)
                    .then(response => {
                        this.responseMessage = response.data.responseDescription; this.canProcess = true;
                        if (response.data.responseCode == '200') {
                            this.postBody = {
                                student:
                                {
                                    Reg_Number:'',
                                    Surname:'',
                                    FirstName:'',
                                    MiddleName:'',
                                    Sex:'',
                                    SchoolCode:'',
                                    CommencementDate:'',
                                    Class:'',
                                    PhoneNumber:'',
                                    Email:'',
                                    Status:'',
                                    ExitDate:'',
                                    ExitReason:'',
                                    Age:'',
                                    ClassCategory:'',
                                    ParentalStatus:''

                                },
                                
                            };
                            alert(`Successfully ${message}`);
                            
                        }
                        else {
                            alert('An error Occured, '+response.data.responseDescription)
                        }
                    })
                    .catch(e => {
                        console.log(e);
                        //this.errors.push(e)
                    });
            }

        },
        
        
        mounted() {
            //this.objectToClear = this.postBody;
                
            // axios
            //     .get('/api/ParentRecord/GetAll')
            //     .then(response => (this.parentList = response.data));
                axios
                .get('/api/SchoolRecord/GetAll')
                .then(response => (this.schoolList = response.data));
                axios
                .get('/api/statictable/getallclass')
                .then(response => (this.classList = response.data));

            if (this.studenttoeditid != 0) {
                alert(this.studenttoeditid)
                axios
                    .get(`/api/StudentRecord/GetStudentById2/${this.studenttoeditid}`)
                    .then(response => {
                        alert(response.data.reg_Number);
                        this.postBody.student.Reg_Number= response.data.reg_Number
                        this.postBody.student.Surname= response.data.surname
                        this.postBody.student.FirstName= response.data.firstName
                        this.postBody.student.MiddleName= response.data.middleName
                        this.postBody.student.Sex= response.data.sex
                        this.postBody.student.SchoolCode= response.data.schoolCode
                        this.postBody.student.CommencementDate= response.data.commencementDate
                        this.postBody.student.Class= response.data.class
                        this.postBody.student.ClassId= response.data.classid
                        this.postBody.student.SchoolId= response.data.schoolId
                        this.postBody.student.Parentid= response.data.parentid
                        this.postBody.student.Guardianid= response.data.guardianid
                        this.postBody.student.PhoneNumber= response.data.phoneNumber
                        this.postBody.student.Email= response.data.email
                        this.postBody.student.Status= response.data.status
                        this.postBody.student.ExitDate= response.data.exitDate
                        this.postBody.student.ExitReason= response.data.exitReason
                        this.postBody.student.Age= response.data.age
                        this.postBody.student.ClassCategory= response.data.classCategory
                        this.postBody.student.ParentalStatus= response.data.parentalStatus
                                        
                    })

            }

        }
    }
</script>
