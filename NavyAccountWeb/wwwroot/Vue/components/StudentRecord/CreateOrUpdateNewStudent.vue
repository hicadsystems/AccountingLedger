

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
                                                    v-model="postBody.student.Guardianid">
                                    </vuejsAutocomplete>

                                </div>
                             </div>
                               

                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Sex</label>
                                <select class="form-control" v-model="postBody.student.Sex" name="rank" required>
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
                                <select class="form-control" v-model="postBody.student.Class" name="Class" required>
                                    <option v-for="cls in classList" v-bind:value="cls.id" v-bind:key="cls.id"> {{ cls.ClassName }} </option>
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label>School</label>
                                <select class="form-control" v-model="postBody.student.SchoolCode" name="SchoolCode" required>
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
                                <select class="form-control" v-model="postBody.student.Status" name="Status" required>
                                    <option v-for="stu in statusList" v-bind:value="stu.value" v-bind:key="stu.value"> {{ stu.text }} </option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label>Enrolment Date</label>
                                <vuejsDatepicker v-model="postBody.student.CommencementDate" input-class="form-control" name="CommencementDate"></vuejsDatepicker>
                            </div>

                             <div class="col-md-4">
                                <label>Exit Left</label>
                                <vuejsDatepicker v-model="postBody.student.ExitDate" input-class="form-control" name="ExitDate"></vuejsDatepicker>
                            </div>
                            <div class="col-md-4">
                                <label>Exit Reason</label>
                                <input class="form-control" name="ExitReason" v-model="postBody.student.ExitReason" />
                            </div>
                        </div>
                      
                        
                    </div>

                </div>
            </form>
        </div>

    </div>

    <!-- END WRAPPER -->
</template>


<script>
    import vuejsAutocomplete from 'vuejs-auto-complete'
    import vuejsDatepicker from 'vuejs-datepicker'
    export default {
        props:['studenttoeditid'],
        components: {
            vuejsDatepicker,
            vuejsAutocomplete
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
                                    Parentid:0,
                                    Guardianid:0
                        
                    },

                },
                
                gender: [
                    { value: 'M', text: 'Male' },
                    { value: 'F', text: 'Female' }
                ],
                statusList: [
                    { value: '1', text: 'Active' },
                    { value: '2', text: 'Inactive' }
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
               
                let uri = actionToPerform == 0 ? `/api/StudentRecord/Add` : `/api/StudentRecord/Update`;
                let message = actionToPerform == 0 ? `Created` : `Updated`;
                alert(this.postBody.student.Reg_Number);
                //alert(this.postBody.person);
              
                axios.post(uri,this.postBody)
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
            this.objectToClear = this.postBody;
                
            axios
                .get('/api/ParentReord/GetAll')
                .then(response => (this.parentList = response.data));
                axios
                .get('/api/SchoolReord/GetAll')
                .then(response => (this.schoolList = response.data));
                axios
                .get('/api/StaticTable/getAllClass')
                .then(response => (this.classList = response.data));

            if (this.studnettoeditid != 0) {
                axios
                    .get(`/api/StudentRecord/getSTudentByID/${this.studenttoeditid}`)
                    .then(response => {
                        alert(response.data.id);
                        this.postBody.student.Reg_Number= response.data.Reg_Number
                        this.postBody.student.Surname= response.data.Surname
                        this.postBody.student.FirstName= response.data.FirstName
                        this.postBody.student.MiddleName= response.data.MiddleName
                        this.postBody.student.Sex= response.data.Sex
                        this.postBody.student.SchoolCode= response.data.SchoolCode
                        this.postBody.student.CommencementDate= response.data.CommencementDate
                        this.postBody.student.Class= response.data.class
                        this.postBody.student.PhoneNumber= response.data.PhoneNumber
                        this.postBody.student.Email= response.data.Email
                        this.postBody.student.Status= response.data.Status
                        this.postBody.student.ExitDate= response.data.ExitDate
                        this.postBody.student.ExitReason= response.data.ExitReason
                        this.postBody.student.Age= response.data.Age
                        this.postBody.student.ClassCategory= response.data.ClassCategory
                        this.postBody.student.ParentalStatus= response.data.ParentalStatus
                                        
                    })

            }

        }
    };
</script>
