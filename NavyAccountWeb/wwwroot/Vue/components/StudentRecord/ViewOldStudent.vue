<template>
    <!-- WRAPPER -->
<div>
    <div class="card-body">
        
        <table id="datatables-buttons" class="table table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>Registration Number</th>
                    <th>Full Name</th>
                    <th>Parent Name </th>
                    <th>Gender</th>
                    <th>Age</th>
                    <th>Class</th>
                    <th>School</th>
                    <!--<th>Beneficiary</th>-->
                    <th>Update</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="student in studentList">
                    <td>{{ student.reg_number }}</td>
                    <td>{{ student.surname}}{{ student.firstName }}  {{ student.middleName }}</td>
                    <td>{{ student.ParentSatus }}</td>
                    <td>{{ getAppropriateGender(student.sex) }}</td>
                    <td>{{ student.age }}</td>
                    <td>{{ student.class }}</td>
                    <td>{{ student.school }}</td>
                   <td><a type="button" :href="'CreatePerson?id='+personel.personID" class="btn btn-submit btn-primary">Edit</a></td>
                </tr>
            </tbody>

        </table>
    </div>

</div>

    <!-- END WRAPPER -->
</template>

<script>
export default {

data() {
    return {
        PersonID:0,
    studentList:null
    };
},
 created() {
        this.$store.state.objectToUpdate = null;
},
mounted () {
    axios
    .get('/api/StudentRecord/getAllInactivePersons')
    .then(response => (this.personelList = response.data))
 },
 methods: {
    

     processRetrieve: function (mainAccount) {

        //this.$store.state.objectToUpdate = mainAccount;
     },

     getAppropriateGender: function (gender) {
         if (gender == 'M')
             return 'Male';
         if (gender == 'F')
             return 'Female';
     }
}

};
</script>